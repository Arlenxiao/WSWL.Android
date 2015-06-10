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
using Android.Widget;

namespace Wswl.BT
{
    [Activity(ConfigurationChanges = Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.Orientation)]	
    public class DeviceListActivity : Activity
    {
        private BluetoothAdapter btAdapter;
        private static ArrayAdapter<string> pairedDevicesArrayAdapter;
        private static ArrayAdapter<string> newDevicesArrayAdapter;
        private Receiver receiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //RequestWindowFeature(WindowFeatures.IndeterminateProgress);
            SetContentView(Resource.Layout.device_list);

            btAdapter = BluetoothAdapter.DefaultAdapter;
            if (btAdapter == null)
            {
                Toast.MakeText(this, "没有蓝牙适配器", ToastLength.Long).Show();
            }

            SetResult(Result.Canceled);

            var scanButton = FindViewById<Button>(Resource.Id.button_scan);
            scanButton.Click += (s, e) =>
            {
                DoDiscovery();
                (s as View).Visibility = ViewStates.Gone;
            };

            pairedDevicesArrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.device_name);
            newDevicesArrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.device_name);

            var pairedListView = FindViewById<ListView>(Resource.Id.paired_devices);
            pairedListView.Adapter = pairedDevicesArrayAdapter;
            pairedListView.ItemClick += DeviceListClick;

            var newDevicesListView = FindViewById<ListView>(Resource.Id.new_devices);
            newDevicesListView.Adapter = newDevicesArrayAdapter;
            newDevicesListView.ItemClick += DeviceListClick;

            receiver = new Receiver(this);
            var filter = new IntentFilter(BluetoothDevice.ActionFound);
            RegisterReceiver(receiver, filter);
            filter = new IntentFilter(BluetoothAdapter.ActionDiscoveryFinished);
            RegisterReceiver(receiver, filter);

            var pairedDevices = btAdapter.BondedDevices;
            if (pairedDevices.Count > 0)
            {
                FindViewById<View>(Resource.Id.title_paired_devices).Visibility = ViewStates.Visible;
                foreach (var device in pairedDevices)
                {
                    pairedDevicesArrayAdapter.Add(device.Name + "\n" + device.Address);
                }
            }
            else
            {
                pairedDevicesArrayAdapter.Add("没有配对设备");
            }

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (btAdapter != null)
            {
                btAdapter.CancelDiscovery();
            }

            UnregisterReceiver(receiver);
        }

        private void DoDiscovery()
        {
            SetProgressBarIndeterminateVisibility(true);

            FindViewById<View>(Resource.Id.title_new_devices).Visibility = ViewStates.Visible;

            if (btAdapter.IsDiscovering)
            {
                btAdapter.CancelDiscovery();
            }

            btAdapter.StartDiscovery();
        }

        void DeviceListClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            btAdapter.CancelDiscovery();

            var info = (e.View as TextView).Text.ToString();
            var address = info.Substring(info.Length - 17);

            Intent intent = new Intent();
            intent.PutExtra("device_address", address);

            SetResult(Result.Ok, intent);
            Finish();
        }

        public class Receiver : BroadcastReceiver
        {
            Activity _chat;

            public Receiver(Activity chat)
            {
                _chat = chat;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                string action = intent.Action;

                if (action == BluetoothDevice.ActionFound)
                {
                    BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                    if (device.BondState != Bond.Bonded)
                    {
                        newDevicesArrayAdapter.Add(device.Name + "\n" + device.Address);
                    }
                }
                else if (action == BluetoothAdapter.ActionDiscoveryFinished)
                {
                    _chat.SetProgressBarIndeterminateVisibility(false);
                    if (newDevicesArrayAdapter.Count == 0)
                    {
                        newDevicesArrayAdapter.Add("未发现设备");
                    }
                }
            }
        }
    }
}