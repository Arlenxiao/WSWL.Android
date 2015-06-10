using System;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Wswl.BT
{
   
	public class MainActivity : Activity
	{
		#region 属性
		/// <summary>蓝牙适配器</summary>
		BluetoothAdapter btAdapter = null;

		private static ArrayAdapter<String> pairedDevicesArrayAdapter;  //配对设备
		private static ArrayAdapter<String> newDevicesArrayAdapter;     //新发现设备

		//private Receiver receiver;
		#endregion

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			//SetContentView(Resource.Layout.Main);

			//InitPage();
		}

		private void InitPage()
		{
            /*
			btAdapter = BluetoothAdapter.DefaultAdapter;
			if (btAdapter == null)
			{
				Toast.MakeText(this, "没有蓝牙适配器", ToastLength.Long).Show();
			}

            var scanButton = FindViewById<Button>(Resource.Id.button_scan);
            scanButton.Click += (sender, e) =>
            {
                DoDiscovery();
                //(sender as View).Visibility = ViewStates.Gone;
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
			*/
		}

		protected override void OnStart()
		{
            //base.OnStart();
            //if (!btAdapter.IsEnabled)
            //{
            //    Intent enableIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
            //    StartActivityForResult(enableIntent, 2);
            //}
		}

		void DeviceListClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			btAdapter.CancelDiscovery();

			var info = (e.View as TextView).Text.ToString();
			var address = info.Substring(info.Length - 17);

			Intent intent = new Intent();
			intent.PutExtra("device_address", address);

			SetResult(Result.Ok, intent);
			//Finish();
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

        /*
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

                // When discovery finds a device
                if (action == BluetoothDevice.ActionFound)
                {
                    // Get the BluetoothDevice object from the Intent
                    BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                    // If it's already paired, skip it, because it's been listed already
                    if (device.BondState != Bond.Bonded)
                    {
                        newDevicesArrayAdapter.Add(device.Name + "\n" + device.Address);
                    }
                    // When discovery is finished, change the Activity title
                }
                else if (action == BluetoothAdapter.ActionDiscoveryFinished)
                {
                    _chat.SetProgressBarIndeterminateVisibility(false);
                    //_chat.SetTitle(Resource.String.select_device);
                    if (newDevicesArrayAdapter.Count == 0)
                    {
                        newDevicesArrayAdapter.Add("未发现设备");
                    }
                }
            }
        }
         */
	}
}

