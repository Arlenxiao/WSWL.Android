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
    [Activity(Label = "注册账号")]
    public class RegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Register);

            //初始绑定化事件
            InitEvent();
        }

        /// <summary>初始化事件</summary>
        private void InitEvent()
        {
            //注册
            var register = FindViewById<Button>(Resource.Id.btn_reg_register);
            register.Click += (s, e) =>
            {
               this.ShowMessage("注册账号");
            };

            //返回
            var back = FindViewById<Button>(Resource.Id.btn_reg_back);
            back.Click += (s, e) =>
            {
                OnBackPressed();
            };
        }
    }
}