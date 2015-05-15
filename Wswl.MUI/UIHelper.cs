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
        /// <summary>带有logo的提示框</summary>
        /// <param name="act"></param>
        /// <param name="msg">要显示的提示消息</param>
        /// <param name="img">图标</param>
        public static void ShowMessage(this Activity act, string msg = "",int img = Resource.Drawable.logo)
        {
            var view = act.LayoutInflater.Inflate(Resource.Layout.app_message, null);//获取Activity里的View
            view.FindViewById<ImageView>(Resource.Id.app_message_image).SetImageResource(img);
            view.FindViewById<TextView>(Resource.Id.app_message_content).Text = msg;

            var toast = new Toast(act) { View = view };
            toast.Show();
        }
    }
}