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
    [Activity(Label = "ע���˺�")]
    public class RegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Register);

            //��ʼ�󶨻��¼�
            InitEvent();
        }

        /// <summary>��ʼ���¼�</summary>
        private void InitEvent()
        {
            //ע��
            var register = FindViewById<Button>(Resource.Id.btn_reg_register);
            register.Click += (s, e) =>
            {
               this.ShowMessage("ע���˺�");
            };

            //����
            var back = FindViewById<Button>(Resource.Id.btn_reg_back);
            back.Click += (s, e) =>
            {
                OnBackPressed();
            };
        }
    }
}