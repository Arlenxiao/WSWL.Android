using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Wswl.MUI.Model;
using Android.Graphics;
using Android.Util;

namespace Wswl.MUI
{
    public static class UIHelper
    {
        /// <summary>带有logo的提示框</summary>
        /// <param name="act"></param>
        /// <param name="msg">要显示的提示消息</param>
        /// <param name="img">图标</param>
        public static void ShowMessage(this Activity act, string msg = "", int img = Resource.Drawable.logo)
        {
            var view = act.LayoutInflater.Inflate(Resource.Layout.app_message, null);//获取Activity里的View
            view.FindViewById<ImageView>(Resource.Id.app_message_image).SetImageResource(img);
            view.FindViewById<TextView>(Resource.Id.app_message_content).Text = msg;

            var toast = new Toast(act) { View = view };
            toast.Show();
        }

        /// <summary>创建开关按钮</summary>
        /// <param name="act">Activity</param>
        /// <param name="metrics">DisplayMetrics</param>
        /// <param name="layoutId">附加布局资源值</param>
        /// <param name="sw">开关类型</param>
        public static void CreateSwitch(this Activity act, DisplayMetrics metrics, Int32 layoutId, ISwitch sw)
        {
            var width = Convert.ToInt32(110 * metrics.Xdpi / 160);
            var height = Convert.ToInt32(80 * metrics.Ydpi / 160);
            var num = Convert.ToInt32(metrics.WidthPixels / width);
            var rows = sw.Count / num + ((sw.Count % num == 0) ? 0 : 1);

            var layout = act.FindViewById<LinearLayout>(layoutId);

            for (var i = 0; i < rows; i++)
            {
                var linearLayout = new LinearLayout(act);
                var btnParams = new LinearLayout.LayoutParams(width, height);
                var n = i * num;
                var g = sw.Count - n;
                var count = g > num ? num : g;
                for (var j = 0; j < count; j++)
                {
                    var pos = sw.Positions[n + j];
                    //Tag改用记录对象实体对象实体需要继承Java.Lang.Object
                    var btn = new Button(act) { Text = pos.Name, LayoutParameters = btnParams, Tag = pos };
                    btnParams.SetMargins(10, 10, 10, 10);
                    btn.SetBackgroundResource(pos.State == 0 ? Resource.Drawable.deviceOffline : Resource.Drawable.selectedTab);
                    btn.SetTextColor(Color.Gray);
                    btn.SetPadding(0, 12, 0, 12);
                    btn.SetCompoundDrawablesWithIntrinsicBounds(0, pos.State == 0 ? Resource.Drawable.icon_device : Resource.Drawable.icon_devices_white, 0, 0);
                    btn.Click += (s, e) =>
                    {
                        var sender = s as Button;
                        if (sender == null) return;

                        Toast.MakeText(act, "点击事件:" + sender.Text, ToastLength.Short).Show();

                        var t = sender.Tag as SwitchPosition;
                        if (t != null)
                        {
                            var state = t.State;//0:关 1:开
                            sender.SetBackgroundResource(state == 0 ? Resource.Drawable.selectedTab : Resource.Drawable.deviceOffline);
                            sender.SetTextColor(state == 0 ? Color.White : Color.Gray);
                            sender.SetCompoundDrawablesWithIntrinsicBounds(0, state == 0 ? Resource.Drawable.icon_device : Resource.Drawable.icon_devices_white, 0, 0);
                            t.State = t.State == 0 ? 1 : 0;
                            sender.Tag = t;
                        }
                    };
                    linearLayout.AddView(btn);
                }
                layout.AddView(linearLayout);
            }
        }

        /// <summary>构建常用设备</summary>
        /// <param name="act">Activity</param>
        /// <param name="metrics">DisplayMetrics</param>
        /// <param name="layoutId">附加布局资源值</param>
        /// <param name="list">设备列表</param>
        /// <param name="isdevices">是否设备总表</param>
        public static void CreateDevice(this Activity act, DisplayMetrics metrics, Int32 layoutId, List<DeviceInfo> list, Boolean isdevices)
        {
            var width = Convert.ToInt32(110 * metrics.Xdpi / 160);
            var height = Convert.ToInt32(80 * metrics.Ydpi / 160);
            var num = Convert.ToInt32(metrics.WidthPixels / width);
            var rows = list.Count / num + ((list.Count % num == 0) ? 0 : 1);

            var isok = false;
            if (isdevices) isok = list.Count % num == 0;//是否刚好一行

            var layout = act.FindViewById<LinearLayout>(layoutId);
            for (var i = 0; i < rows; i++)
            {
                var linearLayout = new LinearLayout(act);
                var btnParams = new LinearLayout.LayoutParams(width, height);
#if DEBUG
                var str = string.Format("Xdpi:{0} px:{1} {2} {3}", metrics.Xdpi, metrics.WidthPixels, width, height);
#endif
                var n = i * num;
                var g = list.Count - n;
                var count = g > num ? num : g;
                for (var j = 0; j < count; j++)
                {
                    var device = list[n + j];
                    var btn = new Button(act) { Text = device.Name, LayoutParameters = btnParams, Tag = (int)device.Type };
                    btnParams.SetMargins(5, 5, 5, 5);
                    btn.SetBackgroundResource(device.IsFind ? Resource.Drawable.selectedTab : Resource.Drawable.deviceOffline);
                    btn.SetTextColor(Color.White);
                    btn.SetPadding(0, 12, 0, 12);
                    btn.SetCompoundDrawablesWithIntrinsicBounds(0, device.Icon, 0, 0);
                    btn.Click += (s, e) =>
                    {
                        var sender = s as Button;
                        if (sender == null) return;
                        var type = Convert.ToInt32(sender.Tag);
                        act.StartActivity(BaseHelper.GetActivityType(type));
                    };
                    linearLayout.AddView(btn);
                    if (isdevices)
                    {
                        var k = rows - 1;
                        var l = count - 1;
                        if (i == k && j == l)
                        {
                            if (!isok)
                            {
                                linearLayout.AddView(CreateFindButton(act, width, height));
                            }
                        }
                    }
                }
                layout.AddView(linearLayout);
            }
            if (!isdevices) return;
            if (isok)
            {
                var linearLayout = new LinearLayout(act);
                linearLayout.AddView(CreateFindButton(act, width, height));
                layout.AddView(linearLayout);
            }
        }

        /// <summary>创建发现设备按钮</summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        private static Button CreateFindButton(Activity act, Int32 width, Int32 height)
        {
            var btnParams = new LinearLayout.LayoutParams(width, height);
            var btn = new Button(act) { Text = "发现设备", LayoutParameters = btnParams, };
            btnParams.SetMargins(5, 5, 5, 5);
            btn.SetBackgroundResource(Resource.Drawable.selectedTab);
            btn.SetTextColor(Color.White);
            btn.SetPadding(0, 12, 0, 12);
            btn.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.icon_search_white, 0, 0);
            btn.Click += (s, e) =>
            {
                var sender = s as Button;
                Toast.MakeText(act, "发现设备" + sender.Text, ToastLength.Short).Show();
            };
            return btn;
        }
    }
}