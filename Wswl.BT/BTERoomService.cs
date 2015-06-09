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
    /// <summary>�����������</summary>
    public class BTERoomService
    {
        private const string TAG = "BTERoomService";
        /// <summary>����������Socket SDP��¼����</summary>
        private const string NAME = "BTEROOM";

        /// <summary>Ӧ�ó���ΨһUUID</summary>
        private static UUID BT_UUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");//�������ڷ���

        protected BluetoothAdapter _adapter;
        protected Handler _handler;
        private AcceptThread acceptThread;
        private ConnectThread connectThread;
        private ConnectedThread connectedThread;
        protected int _state;

        /// <summary>���캯��.׼��һ���µ���������</summary>
        /// <param name="context">ҳ������</param>
        /// <param name="handler">������Ϣ</param>
        public BTERoomService(Context context, Handler handler)
        {
            _adapter = BluetoothAdapter.DefaultAdapter;
            _state = (int)StateEnum.NONE;
            _handler = handler;
        }

        /// <summary>�������ӵ�ǰ״̬</summary>
        /// <param name="state"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetState(int state)
        {
            _state = state;

            // Give the new state to the Handler so the UI Activity can update
            _handler.ObtainMessage((int)MessageEnum.STATE_CHANGE, state, -1).SendToTarget();
        }

        /// <summary>��ȡ��ǰ����״̬</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetState()
        {
            return _state;
        }

        /// <summary>��������</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start()
        {
            // ȡ�������߳�
            if (connectThread != null)
            {
                connectThread.Cancel();
                connectThread = null;
            }

            //ȡ�����Ӻ��߳�
            if (connectedThread != null)
            {
                connectedThread.Cancel();
                connectedThread = null;
            }

            // ȡ�������߳�
            if (acceptThread == null)
            {
                acceptThread = new AcceptThread(this);
                acceptThread.Start();
            }

            SetState((int)StateEnum.LISTEN);
        }

        /// <summary>����Զ�������豸</summary>
        /// <param name="device">���������豸</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect(BluetoothDevice device)
        {
            // ȡ�������߳�
            if (_state == (int)StateEnum.CONNECTING)
            {
                if (connectThread != null)
                {
                    connectThread.Cancel();
                    connectThread = null;
                }
            }

            // ȡ�����Ӻ��߳�
            if (connectedThread != null)
            {
                connectedThread.Cancel();
                connectedThread = null;
            }

            // �����豸�����߳�
            connectThread = new ConnectThread(device, this);
            connectThread.Start();

            SetState((int)StateEnum.CONNECTING);
        }

        /// <summary>��������</summary>
        /// <param name="socket"></param>
        /// <param name="device"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connected(BluetoothSocket socket, BluetoothDevice device)
        {
            // ȡ����������߳�
            if (connectThread != null)
            {
                connectThread.Cancel();
                connectThread = null;
            }

            // ȡ���������߳�
            if (connectedThread != null)
            {
                connectedThread.Cancel();
                connectedThread = null;
            }

            //ȡ�������߳�
            if (acceptThread != null)
            {
                acceptThread.Cancel();
                acceptThread = null;
            }

            // �����̹߳������Ӻʹ���
            connectedThread = new ConnectedThread(socket, this);
            connectedThread.Start();

            // �����豸����
            var msg = _handler.ObtainMessage((int)MessageEnum.DEVICE_NAME);
            var bundle = new Bundle();
            bundle.PutString("�豸��:", device.Name);
            msg.Data = bundle;
            _handler.SendMessage(msg);

            SetState((int)StateEnum.CONNECTED);
        }

        /// <summary>ֹͣ����</summary>
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

        /// <summary>������д��������</summary>
        /// <param name="data">д������</param>
        public void Write(byte[] data)
        {
            ConnectedThread r; // ������ʱ����
            // �������Ӻ��߳�
            lock (this)
            {
                if (_state != (int)StateEnum.CONNECTED)
                    return;
                r = connectedThread;
            }

            r.Write(data);// ͬ��д����
        }

        /// <summary>����ʧ��</summary>
        private void ConnectionFailed()
        {
            SetState((int)StateEnum.LISTEN);

            // ����һ��ʧ����Ϣ������
            var msg = _handler.ObtainMessage((int)MessageEnum.TOAST);
            var bundle = new Bundle();
            bundle.PutString("toast", "�޷������豸");
            msg.Data = bundle;
            _handler.SendMessage(msg);
        }

        /// <summary>�Ͽ��豸����</summary>
        public void ConnectionLost()
        {
            SetState((int)StateEnum.LISTEN);

            // ����һ��ʧ����Ϣ������
            var msg = _handler.ObtainMessage((int)MessageEnum.TOAST);
            var bundle = new Bundle();
            bundle.PutString("toast", "�Ͽ��豸����");
            msg.Data = bundle;
            _handler.SendMessage(msg);
        }

        #region Thread �߳���
        /// <summary>
        /// �����߳�
        /// </summary>
        class AcceptThread : Thread
        {
            private BluetoothServerSocket mmServerSocket;
            private BTERoomService _service;

            public AcceptThread(BTERoomService service)
            {
                _service = service;
                BluetoothServerSocket tmp = null;

                // ����һ���µļ���socket����
                try
                {
                    tmp = _service._adapter.ListenUsingRfcommWithServiceRecord(NAME, BT_UUID);

                }
                catch (Java.IO.IOException e)
                {
                    Log.Error(TAG, "�̼߳���ʧ��", e);
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
                    catch (Exception e) { Log.Error(TAG, "�̼߳���ʧ��", e); break; }

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
                                        catch (Exception e) { Log.Error(TAG, "�ر�Socketʧ��", e); }
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
                catch (Exception e) { Log.Error(TAG, "�ر�Socketʧ��", e); }
            }
        }

        /// <summary>
        /// �����߳�
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
                    Log.Error(TAG, "���Ӵ���ʧ��", e);
                }
                mmSocket = tmp;
            }

            public override void Run()
            {
                Name = "ConnectThread";
                // ���ټ�������
				_service._adapter.CancelDiscovery ();
	
				//����һ������socket����
                try
                {
                    mmSocket.Connect();

                }
                catch (Exception)
                {
                    _service.ConnectionFailed();
                    // �ر�socket
                    try
                    {
                        mmSocket.Close();
                    }
                    catch (Exception e2)
                    {
                        Log.Error(TAG, "����ʧ���޷��ر�����", e2);
                    }

                    // ��������
                    _service.Start();
                    return;
                }
                // ��λ����
                lock (this)
                {
                    _service.connectThread = null;
                }

                // ��������
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
                    Log.Error(TAG, "�ر�����Socketʧ��", e);
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

                // ��ȡ����socket���������
                try
                {
                    tmpIn = socket.InputStream;
                    tmpOut = socket.OutputStream;
                }
                catch (Exception e)
                {
                    Log.Error(TAG, "��������ʧ��", e);
                }

                mmInStream = tmpIn;
                mmOutStream = tmpOut;
            }

            public override void Run()
            {
                byte[] buffer = new byte[1024];
                int bytes;

                // ��������������
                while (true)
                {
                    try
                    {
                        // ��ȡ������
                        bytes = mmInStream.Read(buffer, 0, buffer.Length);

                        // ����淢����Ϣ
                        _service._handler.ObtainMessage((int)MessageEnum.READ, bytes, -1, buffer).SendToTarget();
                    }
                    catch (Exception e)
                    {
                        Log.Error(TAG, "���ӶϿ�", e);
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

                    // ���ؽ��湲����Ϣ
                    _service._handler.ObtainMessage((int)MessageEnum.WRITE, -1, -1, buffer).SendToTarget();
                }
                catch (Exception e)
                {
                    Log.Error(TAG, "�쳣д��", e);
                }
            }

            public void Cancel()
            {
                try
                {
                    mmSocket.Close();
                }
                catch (Exception e) { Log.Error(TAG, "�ر��߳�Socketʧ��", e); }
            }
        }

        #endregion
    }

    /// <summary>
    /// ״̬ö��
    /// </summary>
    public enum StateEnum
    {
        /// <summary>Ĭ��</summary>
        NONE = 0,

        /// <summary>����</summary>
        LISTEN = 1,

        /// <summary>������</summary>
        CONNECTING = 2,

        /// <summary>���Ӻ�</summary>
        CONNECTED = 3,
    }

    /// <summary>
    /// ��Ϣö��
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