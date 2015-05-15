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
    [Activity(Label = "无声物联", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Login);

            //初始化加载
            InitEvent();
        }

        /// <summary>初始化按钮事件</summary>
        private void InitEvent()
        {
            //登录
            var login = FindViewById<Button>(Resource.Id.btn_login_login);
            login.Click += (s, e) => { StartActivity(typeof(MainActivity)); };

            //注册
            var register = FindViewById<Button>(Resource.Id.btn_login_register);
            register.Click += (s, e) => { StartActivity(typeof(RegisterActivity)); };

            //无声物联连接
            var link = FindViewById<Button>(Resource.Id.btn_login_link);
            link.Click += (s, e) =>
            {
                Intent intent = new Intent();
                intent.SetAction("android.intent.action.VIEW");
                var contentUrl = Android.Net.Uri.Parse("http://www.peacemoon.cn/");
                intent.SetData(contentUrl);
                StartActivity(intent);
            };
        }
    }
}