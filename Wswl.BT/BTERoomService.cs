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
        //private AcceptThread acceptThread;
        //protected ConnectThread connectThread;
        //private ConnectedThread connectedThread;
        protected int _state;


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

                // Create a new listening server socket
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
                                    //_service
                                    break;
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
                catch (Exception e) { Log.Error(TAG, "�ر�Socketʧ��", e); break; }
            }
        }

        /// <summary>
        /// �����߳�
        /// </summary>
        private class ConnectThread : Thread
        {
            private BluetoothSocket mmSocket;
            private BluetoothDevice mmDevice;
            private BTERoomService _service;

            public ConnectThread(BluetoothDevice device, BTERoomService service)
            {

            }

            public override void Run()
            {

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

        private class ConnectedThread : Thread
        {
            private BluetoothSocket mmSocket;
            private Stream mmInStream;
            private Stream mmOutStream;
            private BTERoomService _service;

            public ConnectedThread(BluetoothSocket socket, BTERoomService service)
            {
            }

            public override void Run()
            {

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
}