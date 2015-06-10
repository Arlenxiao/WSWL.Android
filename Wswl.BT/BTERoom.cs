using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;

namespace Wswl.BT
{
    /// <summary>
    /// 蓝牙通讯类
    /// </summary>
    [Activity(Label = "无声物联BT", MainLauncher = true, Icon = "@drawable/icon")]
    public class BTERoom : Activity
    {
        #region 属性

        public const string DEVICE_NAME = "设备名：";

        //布局
        private TextView title;
        private ListView conversationView;
        private EditText outEditText;
        private Button sendButton;

        //连接驱动名称
        protected string connectedDeviceName = null;
        //适配器集合
        protected ArrayAdapter<string> conversationArrayAdapter;
        //输出消息
        private StringBuffer outStringBuffer;
        //本地蓝牙适配器
        private BluetoothAdapter bluetoothAdapter = null;
        //蓝牙服务
        private BTERoomService btService = null;

        #endregion

        #region Override

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BTERoom);

            bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (bluetoothAdapter == null)
            {
                Toast.MakeText(this, "没有蓝牙适配器", ToastLength.Long).Show();
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (!bluetoothAdapter.IsEnabled)
            {
                Intent enableIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(enableIntent, (int)RequestEnum.ENABLE_BT);
            }
            else
            {
                if (btService == null)
                    InitRoom();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (btService != null)
            {
                if (btService.GetState() == (int)StateEnum.NONE)
                {
                    btService.Start();// 启动蓝牙服务
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (btService != null)
                btService.Stop();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case (int)RequestEnum.CONNECT_DEVICE:
                {
                    if (resultCode == Result.Ok)
                    {
                        var address = data.Extras.GetString("device_address");
                        BluetoothDevice device = bluetoothAdapter.GetRemoteDevice(address);
                        btService.Connect(device);
                    }
                    break;
                }
                case (int)RequestEnum.ENABLE_BT:
                {
                    if (resultCode == Result.Ok)
                    {
                        InitRoom();
                    }
                    else
                    {
                        Toast.MakeText(this, "离开", ToastLength.Short).Show();
                        Finish();
                    }
                    break;
                }
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.option_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.scan:
                {
                    var serverIntent = new Intent(this, typeof(DeviceListActivity));
                    StartActivityForResult(serverIntent, (int)RequestEnum.CONNECT_DEVICE);
                    return true;
                }
                case Resource.Id.discoverable:
                {
                    EnsureDiscoverable();
                    return true;
                }
            }
            return false;
        }

        private void EnsureDiscoverable()
        {
            if (bluetoothAdapter.ScanMode != ScanMode.ConnectableDiscoverable)
            {
                Intent discoverableIntent = new Intent(BluetoothAdapter.ActionRequestDiscoverable);
                discoverableIntent.PutExtra(BluetoothAdapter.ExtraDiscoverableDuration, 300);
                StartActivity(discoverableIntent);
            }
        }
        #endregion

        #region 私有方法

        private void SendMessage(Java.Lang.String message)
        {
            if (btService.GetState() != (int)StateEnum.CONNECTED)
            {
                Toast.MakeText(this, "设备未连接", ToastLength.Short).Show();
                return;
            }
            if (message.Length() > 0)
            {
                var send = message.GetBytes();
                btService.Write(send);

                outStringBuffer.SetLength(0);
                outEditText.Text = string.Empty;
            }
        }

        private void InitRoom()
        {
            conversationArrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.message);
            conversationView = FindViewById<ListView>(Resource.Id.lv_in);
            conversationView.Adapter = conversationArrayAdapter;

            outEditText = FindViewById<EditText>(Resource.Id.edit_text_out);
            outEditText.EditorAction += delegate(object sender, TextView.EditorActionEventArgs e)
            {
                if (e.ActionId == ImeAction.ImeNull && e.Event.Action == KeyEventActions.Up)
                {
                    var message = new Java.Lang.String(((TextView)sender).Text);
                    SendMessage(message);
                }
            };

            sendButton = FindViewById<Button>(Resource.Id.button_send);
            sendButton.Click += (s, e) =>
            {
                var view = FindViewById<TextView>(Resource.Id.edit_text_out);
                var message = new Java.Lang.String(view.Text);
                SendMessage(message);
            };

            btService = new BTERoomService(this, new BTHandler(this));
            outStringBuffer = new StringBuffer("");
        }

        #endregion

        #region 内部类

        class BTHandler : Handler
        {
            private BTERoom btRoom;

            public BTHandler(BTERoom bt)
            {
                btRoom = bt;
            }

            public override void HandleMessage(Message msg)
            {
                switch (msg.What)
                {
                    case (int)StateEnum.CONNECTED:
                    {

                        break;
                    }
                }
            }
        }

        #endregion

    }
}