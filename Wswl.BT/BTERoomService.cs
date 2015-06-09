using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util;
using Java.Lang;
using Exception = System.Exception;
using System.IO;
using System.Runtime.CompilerServices;

namespace Wswl.BT
{
    /// <summary>蓝牙房间服务</summary>
    public class BTERoomService
    {
        private const string TAG = "BTERoomService";
        /// <summary>创建服务器Socket SDP记录名字</summary>
        private const string NAME = "BTEROOM";

        /// <summary>应用程序唯一UUID</summary>
        private static UUID BT_UUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");//蓝牙串口服务

        protected BluetoothAdapter _adapter;
        protected Handler _handler;
        private AcceptThread acceptThread;
        private ConnectThread connectThread;
        private ConnectedThread connectedThread;
        protected int _state;

        /// <summary>构造函数.准备一个新的蓝牙服务</summary>
        /// <param name="context">页面内容</param>
        /// <param name="handler">处理消息</param>
        public BTERoomService(Context context, Handler handler)
        {
            _adapter = BluetoothAdapter.DefaultAdapter;
            _state = (int)StateEnum.NONE;
            _handler = handler;
        }

        /// <summary>设置连接当前状态</summary>
        /// <param name="state"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetState(int state)
        {
            _state = state;

            // Give the new state to the Handler so the UI Activity can update
            _handler.ObtainMessage((int)MessageEnum.STATE_CHANGE, state, -1).SendToTarget();
        }

        /// <summary>获取当前连接状态</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetState()
        {
            return _state;
        }

        /// <summary>启动服务</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start()
        {
            // 取消连接线程
            if (connectThread != null)
            {
                connectThread.Cancel();
                connectThread = null;
            }

            //取消连接后线程
            if (connectedThread != null)
            {
                connectedThread.Cancel();
                connectedThread = null;
            }

            // 取消监听线程
            if (acceptThread == null)
            {
                acceptThread = new AcceptThread(this);
                acceptThread.Start();
            }

            SetState((int)StateEnum.LISTEN);
        }

        /// <summary>连接远程蓝牙设备</summary>
        /// <param name="device">连接蓝牙设备</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect(BluetoothDevice device)
        {
            // 取消连接线程
            if (_state == (int)StateEnum.CONNECTING)
            {
                if (connectThread != null)
                {
                    connectThread.Cancel();
                    connectThread = null;
                }
            }

            // 取消连接后线程
            if (connectedThread != null)
            {
                connectedThread.Cancel();
                connectedThread = null;
            }

            // 根据设备启动线程
            connectThread = new ConnectThread(device, this);
            connectThread.Start();

            SetState((int)StateEnum.CONNECTING);
        }

        /// <summary>连接蓝牙</summary>
        /// <param name="socket"></param>
        /// <param name="device"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connected(BluetoothSocket socket, BluetoothDevice device)
        {
            // 取消完成连接线程
            if (connectThread != null)
            {
                connectThread.Cancel();
                connectThread = null;
            }

            // 取消连接中线程
            if (connectedThread != null)
            {
                connectedThread.Cancel();
                connectedThread = null;
            }

            //取消监听线程
            if (acceptThread != null)
            {
                acceptThread.Cancel();
                acceptThread = null;
            }

            // 启动线程管理连接和传输
            connectedThread = new ConnectedThread(socket, this);
            connectedThread.Start();

            // 发送设备名称
            var msg = _handler.ObtainMessage((int)MessageEnum.DEVICE_NAME);
            var bundle = new Bundle();
            bundle.PutString("设备名:", device.Name);
            msg.Data = bundle;
            _handler.SendMessage(msg);

            SetState((int)StateEnum.CONNECTED);
        }

        /// <summary>停止蓝牙</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Stop()
        {
            if (connectThread != null)
            {
                connectThread.Cancel();
                connectThread = null;
            }

            if (connectedThread != null)
            {
                connectedThread.Cancel();
                connectedThread = null;
            }

            if (acceptThread != null)
            {
                acceptThread.Cancel();
                acceptThread = null;
            }

            SetState((int)StateEnum.NONE);
        }

        /// <summary>把数据写入数据流</summary>
        /// <param name="data">写入数据</param>
        public void Write(byte[] data)
        {
            ConnectedThread r; // 创建临时对象
            // 复制连接后线程
            lock (this)
            {
                if (_state != (int)StateEnum.CONNECTED)
                    return;
                r = connectedThread;
            }

            r.Write(data);// 同步写数据
        }

        /// <summary>连接失败</summary>
        private void ConnectionFailed()
        {
            SetState((int)StateEnum.LISTEN);

            // 发送一个失败信息到界面
            var msg = _handler.ObtainMessage((int)MessageEnum.TOAST);
            var bundle = new Bundle();
            bundle.PutString("toast", "无法连接设备");
            msg.Data = bundle;
            _handler.SendMessage(msg);
        }

        /// <summary>断开设备连接</summary>
        public void ConnectionLost()
        {
            SetState((int)StateEnum.LISTEN);

            // 发送一个失败消息到界面
            var msg = _handler.ObtainMessage((int)MessageEnum.TOAST);
            var bundle = new Bundle();
            bundle.PutString("toast", "断开设备连接");
            msg.Data = bundle;
            _handler.SendMessage(msg);
        }

        #region Thread 线程类
        /// <summary>
        /// 监听线程
        /// </summary>
        class AcceptThread : Thread
        {
            private BluetoothServerSocket mmServerSocket;
            private BTERoomService _service;

            public AcceptThread(BTERoomService service)
            {
                _service = service;
                BluetoothServerSocket tmp = null;

                // 创建一个新的监听socket连接
                try
                {
                    tmp = _service._adapter.ListenUsingRfcommWithServiceRecord(NAME, BT_UUID);

                }
                catch (Java.IO.IOException e)
                {
                    Log.Error(TAG, "线程监听失败", e);
                }
                mmServerSocket = tmp;
            }

            public override void Run()
            {
                Name = "AcceptThread";
                BluetoothSocket socket = null;
                while (_service._state != (int)StateEnum.CONNECTED)
                {
                    try
                    {
                        socket = mmServerSocket.Accept();
                    }
                    catch (Exception e) { Log.Error(TAG, "线程监听失败", e); break; }

                    if (socket != null)
                    {
                        lock (this)
                        {
                            switch (_service._state)
                            {
                                case (int)StateEnum.LISTEN:
                                case (int)StateEnum.CONNECTING:
                                    {
                                        _service.Connected(socket, socket.RemoteDevice);
                                        break;
                                    }
                                case (int)StateEnum.NONE:
                                case (int)StateEnum.CONNECTED:
                                    {
                                        try
                                        {
                                            socket.Close();
                                        }
                                        catch (Exception e) { Log.Error(TAG, "关闭Socket失败", e); }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }

            public void Cancel()
            {
                try
                {
                    mmServerSocket.Close();
                }
                catch (Exception e) { Log.Error(TAG, "关闭Socket失败", e); }
            }
        }

        /// <summary>
        /// 连接线程
        /// </summary>
        class ConnectThread : Thread
        {
            private BluetoothSocket mmSocket;
            private BluetoothDevice mmDevice;
            private BTERoomService _service;

            public ConnectThread(BluetoothDevice device, BTERoomService service)
            {
                mmDevice = device;
                _service = service;
                BluetoothSocket tmp = null;
                try
                {
                    tmp = device.CreateRfcommSocketToServiceRecord(BT_UUID);
                }
                catch (Exception e)
                {
                    Log.Error(TAG, "连接创建失败", e);
                }
                mmSocket = tmp;
            }

            public override void Run()
            {
                Name = "ConnectThread";
                // 销毁减慢连接
				_service._adapter.CancelDiscovery ();
	
				//创建一个蓝牙socket连接
                try
                {
                    mmSocket.Connect();

                }
                catch (Exception)
                {
                    _service.ConnectionFailed();
                    // 关闭socket
                    try
                    {
                        mmSocket.Close();
                    }
                    catch (Exception e2)
                    {
                        Log.Error(TAG, "连接失败无法关闭连接", e2);
                    }

                    // 启动服务
                    _service.Start();
                    return;
                }
                // 复位操作
                lock (this)
                {
                    _service.connectThread = null;
                }

                // 启动连接
                _service.Connected(mmSocket, mmDevice);
            }

            public void Cancel()
            {
                try
                {
                    mmSocket.Close();
                }
                catch (Exception e)
                {
                    Log.Error(TAG, "关闭连接Socket失败", e);
                }
            }
        }

        class ConnectedThread : Thread
        {
            private BluetoothSocket mmSocket;
            private Stream mmInStream;
            private Stream mmOutStream;
            private BTERoomService _service;

            public ConnectedThread(BluetoothSocket socket, BTERoomService service)
            {
                mmSocket = socket;
                _service = service;
                Stream tmpIn = null;
                Stream tmpOut = null;

                // 获取蓝牙socket输入输出流
                try
                {
                    tmpIn = socket.InputStream;
                    tmpOut = socket.OutputStream;
                }
                catch (Exception e)
                {
                    Log.Error(TAG, "创建连接失败", e);
                }

                mmInStream = tmpIn;
                mmOutStream = tmpOut;
            }

            public override void Run()
            {
                byte[] buffer = new byte[1024];
                int bytes;

                // 保持输入流监听
                while (true)
                {
                    try
                    {
                        // 读取输入流
                        bytes = mmInStream.Read(buffer, 0, buffer.Length);

                        // 向界面发送信息
                        _service._handler.ObtainMessage((int)MessageEnum.READ, bytes, -1, buffer).SendToTarget();
                    }
                    catch (Exception e)
                    {
                        Log.Error(TAG, "连接断开", e);
                        _service.ConnectionLost();
                        break;
                    }
                }
            }

            public void Write(byte[] buffer)
            {
                try
                {
                    mmOutStream.Write(buffer, 0, buffer.Length);

                    // 返回界面共享信息
                    _service._handler.ObtainMessage((int)MessageEnum.WRITE, -1, -1, buffer).SendToTarget();
                }
                catch (Exception e)
                {
                    Log.Error(TAG, "异常写入", e);
                }
            }

            public void Cancel()
            {
                try
                {
                    mmSocket.Close();
                }
                catch (Exception e) { Log.Error(TAG, "关闭线程Socket失败", e); }
            }
        }

        #endregion
    }

    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum StateEnum
    {
        /// <summary>默认</summary>
        NONE = 0,

        /// <summary>监听</summary>
        LISTEN = 1,

        /// <summary>连接中</summary>
        CONNECTING = 2,

        /// <summary>连接后</summary>
        CONNECTED = 3,
    }

    /// <summary>
    /// 消息枚举
    /// </summary>
    public enum MessageEnum
    {
        /// <summary></summary>
        STATE_CHANGE = 1,

        /// <summary></summary>
        READ = 2,

        /// <summary></summary>
        WRITE = 3,

        /// <summary></summary>
        DEVICE_NAME = 4,

        /// <summary></summary>
        TOAST = 5,

    }
}