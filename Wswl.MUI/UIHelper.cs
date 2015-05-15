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
    public static class UIHelper
    {
        /// <summary>����logo����ʾ��</summary>
        /// <param name="act"></param>
        /// <param name="msg">Ҫ��ʾ����ʾ��Ϣ</param>
        /// <param name="img">ͼ��</param>
        public static void ShowMessage(this Activity act, string msg = "",int img = Resource.Drawable.logo)
        {
            var view = act.LayoutInflater.Inflate(Resource.Layout.app_message, null);//��ȡActivity���View
            view.FindViewById<ImageView>(Resource.Id.app_message_image).SetImageResource(img);
            view.FindViewById<TextView>(Resource.Id.app_message_content).Text = msg;

            var toast = new Toast(act) { View = view };
            toast.Show();
        }
    }
}