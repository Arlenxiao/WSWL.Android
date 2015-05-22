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
    [Activity(Label = "主页")]
    public class HomeActivity : Activity
    {


        public HomeActivity()
        {
            DeciveList = new List<DeviceInfo>
            {
                new DeviceInfo{Name = "网关",Icon= Resource.Drawable.icon_gateway,IsFind = true,Type=DeviceType.W0103},
                new DeviceInfo{Name = "喝茶室开关",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "办公区开关",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "大厅红外",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.A0541},
                new DeviceInfo{Name = "前门开关",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "喝茶室门窗磁",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.A0501},
            };
        }

        #region 属性

        private List<DeviceInfo> DeciveList { get; set; }

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Home);

            //初始化事件
            InitEvent();

            //构建常用设备
            this.CreateDevice(Resources.DisplayMetrics, Resource.Id.layout_home_devices, DeciveList, false);
        }

        /// <summary>初始化事件</summary>
        private void InitEvent()
        {
            //网关菜单
            FindViewById<ImageView>(Resource.Id.home_menu_gateway).Click += (s, e) => { Toast.MakeText(this, "网关菜单", ToastLength.Short).Show(); };

            //报警提示
            FindViewById<TextView>(Resource.Id.lbl_home_alarm).Click += (s, e) =>
            {
                var tab = this.Parent as MainActivity;
                if (tab != null) tab.SetCurrentTab("NewsActivity");
            };

            //设置按钮
            FindViewById<ImageView>(Resource.Id.img_home_icon_setting).Click += (s, e) =>
            {
                //StartActivity(typeof(ManagesActivity));
                var tab = this.Parent as MainActivity;
                if (tab != null) tab.SetCurrentTab("ManagesActivity");
            };

            //报警信息
            FindViewById<TextView>(Resource.Id.lbl_home_warning).Click += (s, e) =>
            {
                var tab = this.Parent as MainActivity;
                if (tab != null) tab.SetCurrentTab("NewsActivity");
            };

            //在线设备统计
            FindViewById<Button>(Resource.Id.btn_home_online_devices).Click += (s, e) => { Toast.MakeText(this, "当前在线设备:7/21", ToastLength.Short).Show(); };

            //在线用户统计
            FindViewById<Button>(Resource.Id.btn_home_online_user).Click += (s, e) => { Toast.MakeText(this, "当前在线设备用户:1/6", ToastLength.Short).Show(); };
            /*
            //W0103
            FindViewById<Button>(Resource.Id.btn_home_dev_w0103_01).Click += (s, e) => { Toast.MakeText(this, "网关W0103,暂时还没有做具体界面,等待做好跳转对应控制界面!", ToastLength.Short).Show(); };

            //茶室开关
            FindViewById<Button>(Resource.Id.btn_home_dev_k0203_01).Click += (s, e) => { Toast.MakeText(this, "茶室开关,暂时还没有做具体界面,等待做好跳转对应控制界面!", ToastLength.Short).Show(); };

            //大厅红外
            FindViewById<Button>(Resource.Id.btn_home_dev_A0541_01).Click += (s, e) => { Toast.MakeText(this, "大厅红外,暂时还没有做具体界面,等待做好跳转对应控制界面!", ToastLength.Short).Show(); };

            //办公室开关
            FindViewById<Button>(Resource.Id.btn_home_dev_k0203_02).Click += (s, e) => { Toast.MakeText(this, "办公室开关,暂时还没有做具体界面,等待做好跳转对应控制界面!", ToastLength.Short).Show(); };

            //车间红外
            FindViewById<Button>(Resource.Id.btn_home_dev_A0541_02).Click += (s, e) => { Toast.MakeText(this, "车间红外,暂时还没有做具体界面,等待做好跳转对应控制界面!", ToastLength.Short).Show(); };

            //茶室门窗磁
            FindViewById<Button>(Resource.Id.btn_home_dev_A0501_01).Click += (s, e) => { Toast.MakeText(this, "茶室门窗磁,暂时还没有做具体界面,等待做好跳转对应控制界面!", ToastLength.Short).Show(); };
            */
        }
    }

}