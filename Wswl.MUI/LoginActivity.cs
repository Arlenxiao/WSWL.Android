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
            SetTheme(WswlVariable.AppTheme);
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Login);

            //初始化加载
            InitEvent();

            CheckedRadioButton();
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

            //
            var def = FindViewById<RadioButton>(Resource.Id.rd_login_default);
            def.Click += (s, e) => { SetAppTheme(WswlTheme.DEFAULT); };
            var black = FindViewById<RadioButton>(Resource.Id.rd_login_black);
            black.Click += (s, e) => { SetAppTheme(WswlTheme.BLACK); };
            var green = FindViewById<RadioButton>(Resource.Id.rd_login_green);
            green.Click += (s, e) => { SetAppTheme(WswlTheme.GREEN); };
            var pink = FindViewById<RadioButton>(Resource.Id.rd_login_pink);
            pink.Click += (s, e) => { SetAppTheme(WswlTheme.PINK); };
        }

        private void SetAppTheme(Int32 theme)
        {
            WswlVariable.AppTheme = theme;
            var activity = new Intent(this, typeof(LoginActivity));
            Finish();
            StartActivity(activity);
        }

        private void CheckedRadioButton()
        {
            switch (WswlVariable.AppTheme)
            {
                case WswlTheme.DEFAULT: { 
                    var def = FindViewById<RadioButton>(Resource.Id.rd_login_default);
                    def.Checked = true;
                    break; }
                case WswlTheme.BLACK:
                {
                    var black = FindViewById<RadioButton>(Resource.Id.rd_login_black);
                    black.Checked = true;
                    break;
                }
                case WswlTheme.GREEN:
                {
                    var green = FindViewById<RadioButton>(Resource.Id.rd_login_green);
                    green.Checked = true;
                    break;
                }
                case WswlTheme.PINK:
                {
                    var pink = FindViewById<RadioButton>(Resource.Id.rd_login_pink);
                    pink.Checked = true;
                    break;
                }
            }
        }
    }
}