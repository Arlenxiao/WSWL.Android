using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Wswl.MUI
{
    [Activity(Label = "主页")]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Home);

            //初始化事件
            InitEvent();
        }

        /// <summary>初始化事件</summary>
        private void InitEvent()
        {
            //网关菜单
            FindViewById<ImageView>(Resource.Id.home_menu_gateway).Click += (s, e) => { Toast.MakeText(this, "网关菜单", ToastLength.Short).Show(); };

            //报警提示
            FindViewById<TextView>(Resource.Id.lbl_home_alarm).Click += (s, e) => { Toast.MakeText(this, "报警提示--36", ToastLength.Long).Show(); };

            //设置按钮
            FindViewById<ImageView>(Resource.Id.img_home_icon_setting).Click += (s, e) => { Toast.MakeText(this, "设置管理", ToastLength.Short).Show(); };

            //报警信息
            FindViewById<TextView>(Resource.Id.lbl_home_warning).Click += (s, e) => { Toast.MakeText(this, "报警信息--喝茶室门窗磁报警", ToastLength.Short).Show(); };

            //在线设备统计
            FindViewById<Button>(Resource.Id.btn_home_online_devices).Click += (s, e) => { Toast.MakeText(this, "当前在线设备:7/21", ToastLength.Short).Show(); };

            //在线用户统计
            FindViewById<Button>(Resource.Id.btn_home_online_user).Click += (s, e) => { Toast.MakeText(this, "当前在线设备用户:1/6", ToastLength.Short).Show(); };

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
        }
    }
}