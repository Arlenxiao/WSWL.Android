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
using Android.Graphics;

namespace Wswl.MUI
{
    [Activity(Label = "K0203Activity")]
    public class K0203Activity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.K0203);

            //��ʼ������
            InitSwitch(new Random().Next(0, 10));
        }

        /// <summary>��ʼ������</summary>
        private void InitSwitch(Int32 type)
        {

            var width = Convert.ToInt32(110 * Resources.DisplayMetrics.Xdpi / 160);
            var height = Convert.ToInt32(80 * Resources.DisplayMetrics.Ydpi / 160);
            var num = Convert.ToInt32(Resources.DisplayMetrics.WidthPixels / width);
            var rows = type / num + ((type % num == 0) ? 0 : 1);

            var layout = FindViewById<LinearLayout>(Resource.Id.k0203_layout_content);

            for (var i = 0; i < rows; i++)
            {
                var linearLayout = new LinearLayout(this);
                var btnParams = new LinearLayout.LayoutParams(width, height);
                var n = i * num;
                var g = type - n;
                var count = g > num ? num : g;
                for (var j = 0; j < count; j++)
                {
                    var btn = new Button(this) { Text = "����" + (n + j), LayoutParameters = btnParams, Tag = 0 };
                    btnParams.SetMargins(10, 10, 10, 10);
                    btn.SetBackgroundResource(Resource.Drawable.deviceOffline);
                    btn.SetTextColor(Color.Gray);
                    btn.SetPadding(0, 12, 0, 12);
                    btn.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.icon_device, 0, 0);
                    btn.Click += (s, e) =>
                    {
                        var sender = s as Button;
                        if (sender == null) return;

                        Toast.MakeText(this, "����¼�:" + sender.Text, ToastLength.Short).Show();

                        var t = sender.Tag;
                        var state = Convert.ToInt32(t ?? 0);//0:�� 1:��
                        sender.SetBackgroundResource(state == 0 ? Resource.Drawable.selectedTab : Resource.Drawable.deviceOffline);
                        sender.SetTextColor(state == 0 ? Color.White : Color.Gray);
                        sender.SetCompoundDrawablesWithIntrinsicBounds(0, state == 0 ? Resource.Drawable.icon_device : Resource.Drawable.icon_devices_white, 0, 0);
                        sender.Tag = state == 0 ? 1 : 0;
                    };
                    linearLayout.AddView(btn);
                }
                layout.AddView(linearLayout);
            }
        }
    }
}