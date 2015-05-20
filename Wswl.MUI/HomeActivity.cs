using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net.Wifi.P2p;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Wswl.MUI.Model;

namespace Wswl.MUI
{
    [Activity(Label = "��ҳ")]
    public class HomeActivity : Activity
    {


        public HomeActivity()
        {
            DeciveList = new List<DeviceInfo>
            {
                new DeviceInfo{Name = "����",Icon= Resource.Drawable.icon_gateway,IsFind = true},
                new DeviceInfo{Name = "�Ȳ��ҿ���",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "�칫������",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "��������",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "ǰ�ſ���",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "�Ȳ����Ŵ���",Icon= Resource.Drawable.icon_device,IsFind = true},
            };
        }

        #region ����

        private List<DeviceInfo> DeciveList { get; set; }

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Home);

            //��ʼ���¼�
            InitEvent();

            //���������豸
            CreateDevice();
        }

        /// <summary>��ʼ���¼�</summary>
        private void InitEvent()
        {
            //���ز˵�
            FindViewById<ImageView>(Resource.Id.home_menu_gateway).Click += (s, e) => { Toast.MakeText(this, "���ز˵�", ToastLength.Short).Show(); };

            //������ʾ
            FindViewById<TextView>(Resource.Id.lbl_home_alarm).Click += (s, e) =>
            {
                var tab = this.Parent as MainActivity;
                if (tab != null) tab.SetCurrentTab("NewsActivity");
            };

            //���ð�ť
            FindViewById<ImageView>(Resource.Id.img_home_icon_setting).Click += (s, e) =>
            {
                //StartActivity(typeof(ManagesActivity));
                var tab = this.Parent as MainActivity;
                if (tab != null) tab.SetCurrentTab("ManagesActivity");
            };

            //������Ϣ
            FindViewById<TextView>(Resource.Id.lbl_home_warning).Click += (s, e) =>
            {
                var tab = this.Parent as MainActivity;
                if (tab != null) tab.SetCurrentTab("NewsActivity");
            };

            //�����豸ͳ��
            FindViewById<Button>(Resource.Id.btn_home_online_devices).Click += (s, e) => { Toast.MakeText(this, "��ǰ�����豸:7/21", ToastLength.Short).Show(); };

            //�����û�ͳ��
            FindViewById<Button>(Resource.Id.btn_home_online_user).Click += (s, e) => { Toast.MakeText(this, "��ǰ�����豸�û�:1/6", ToastLength.Short).Show(); };
            /*
            //W0103
            FindViewById<Button>(Resource.Id.btn_home_dev_w0103_01).Click += (s, e) => { Toast.MakeText(this, "����W0103,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //���ҿ���
            FindViewById<Button>(Resource.Id.btn_home_dev_k0203_01).Click += (s, e) => { Toast.MakeText(this, "���ҿ���,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //��������
            FindViewById<Button>(Resource.Id.btn_home_dev_A0541_01).Click += (s, e) => { Toast.MakeText(this, "��������,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //�칫�ҿ���
            FindViewById<Button>(Resource.Id.btn_home_dev_k0203_02).Click += (s, e) => { Toast.MakeText(this, "�칫�ҿ���,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //�������
            FindViewById<Button>(Resource.Id.btn_home_dev_A0541_02).Click += (s, e) => { Toast.MakeText(this, "�������,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };

            //�����Ŵ���
            FindViewById<Button>(Resource.Id.btn_home_dev_A0501_01).Click += (s, e) => { Toast.MakeText(this, "�����Ŵ���,��ʱ��û�����������,�ȴ�������ת��Ӧ���ƽ���!", ToastLength.Short).Show(); };
            */
        }

        /// <summary>���������豸</summary>
        private void CreateDevice()
        {
            var width = Convert.ToInt32(110 * Resources.DisplayMetrics.Xdpi / 160);
            var height = Convert.ToInt32(80 * Resources.DisplayMetrics.Ydpi / 160);
            var num = Convert.ToInt32(Resources.DisplayMetrics.WidthPixels / width);
            var rows = DeciveList.Count / num + ((DeciveList.Count % num == 0) ? 0 : 1);

            var layout = FindViewById<LinearLayout>(Resource.Id.layout_home_devices);
            for (var i = 0; i < rows; i++)
            {
                var linearLayout = new LinearLayout(this);
                var btnParams = new LinearLayout.LayoutParams(width, height);
#if DEBUG
                var str = string.Format("Xdpi:{0} px:{1} {2} {3}", Resources.DisplayMetrics.Xdpi, Resources.DisplayMetrics.WidthPixels, width, height);
#endif
                var n = i * num;
                var g = DeciveList.Count - n;
                var count = g > num ? num : g;
                for (var j = 0; j < count; j++)
                {
                    var device = DeciveList[n + j];
                    var btn = new Button(this) { Text = device.Name, LayoutParameters = btnParams, };
                    btnParams.SetMargins(5, 5, 5, 5);
                    btn.SetBackgroundResource(device.IsFind ? Resource.Drawable.selectedTab : Resource.Drawable.deviceOffline);
                    btn.SetTextColor(Color.White);
                    btn.SetPadding(0, 12, 0, 12);
                    //btn.SetCompoundDrawablesWithIntrinsicBounds(i == 0 ? Resource.Drawable.icon_gateway : 0, 0, i == 1 ? Resource.Drawable.icon_gateway : 0, 0);
                    btn.SetCompoundDrawablesWithIntrinsicBounds(0, device.Icon, 0, 0);
                    btn.Click += (s, e) =>
                    {
                        var sender = s as Button;
#if DEBUG
                        Toast.MakeText(this, str + "  " + sender.Text, ToastLength.Short).Show();
#endif
                    };
                    linearLayout.AddView(btn);
                }
                layout.AddView(linearLayout);
            }
#if DEBUG
            var linearLayout_debug = new LinearLayout(this);
            var btnParams_debug = new LinearLayout.LayoutParams(width, height);
            var str_debug = string.Format("Xdpi:{0} px:{1} {2} {3}", Resources.DisplayMetrics.Xdpi, Resources.DisplayMetrics.WidthPixels, width, height);

            for (var j = 0; j < num; j++)
            {
                var btn = new Button(this) { Text = "��̬�豸" + j, LayoutParameters = btnParams_debug, };
                btnParams_debug.SetMargins(5, 5, 5, 5);
                btn.SetBackgroundResource(Resource.Drawable.selectedTab);
                btn.SetTextColor(Color.White);
                btn.SetPadding(0, 12, 0, 12);
                btn.SetCompoundDrawablesWithIntrinsicBounds(j % 2 == 0 ? Resource.Drawable.icon_gateway : 0, 0, j == 1 ? Resource.Drawable.icon_gateway : 0, 0);
                btn.Click += (s, e) =>
                {
                    var sender = s as Button;
                    Toast.MakeText(this, str_debug + "  " + sender.Text, ToastLength.Short).Show();

                };
                linearLayout_debug.AddView(btn);
            }
            layout.AddView(linearLayout_debug);
#endif
        }
    }

}