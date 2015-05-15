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
    [Activity(Label = "��������", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Login);

            //��ʼ������
            InitEvent();
        }

        /// <summary>��ʼ����ť�¼�</summary>
        private void InitEvent()
        {
            //��¼
            var login = FindViewById<Button>(Resource.Id.btn_login_login);
            login.Click += (s, e) => { StartActivity(typeof(MainActivity)); };

            //ע��
            var register = FindViewById<Button>(Resource.Id.btn_login_register);
            register.Click += (s, e) => { StartActivity(typeof(RegisterActivity)); };

            //������������
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