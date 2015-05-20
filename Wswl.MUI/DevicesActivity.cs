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
using Wswl.MUI.Model;
using Android.Graphics;

namespace Wswl.MUI
{
    [Activity(Label = "设备列表")]
    public class DevicesActivity : Activity
    {



        public DevicesActivity()
        {
            DeciveList = new List<DeviceInfo>
            {
                new DeviceInfo{Name = "网关",Icon= Resource.Drawable.icon_gateway,IsFind = true},
                new DeviceInfo{Name = "喝茶室开关",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "办公区开关",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "大厅红外",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "前门开关",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "喝茶室门窗磁",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "智能继电器",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "触摸开关",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "红外感应器",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "门窗磁",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "环境探测器",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "调色控制器",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "智能门锁",Icon= Resource.Drawable.icon_device,IsFind = true},
            };
        }

        #region 属性

        private List<DeviceInfo> DeciveList { get; set; }

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Devices);

            //构建常用设备
            CreateDevice();
        }



        /// <summary>构建常用设备</summary>
        private void CreateDevice()
        {
            var width = Convert.ToInt32(110 * Resources.DisplayMetrics.Xdpi / 160);
            var height = Convert.ToInt32(80 * Resources.DisplayMetrics.Ydpi / 160);
            var num = Convert.ToInt32(Resources.DisplayMetrics.WidthPixels / width);
            var rows = DeciveList.Count / num + ((DeciveList.Count % num == 0) ? 0 : 1);

            var isok = DeciveList.Count % num == 0;//是否刚好一行

            var layout = FindViewById<LinearLayout>(Resource.Id.deveces_content);
            for (var i = 0; i < rows; i++)
            {
                var linearLayout = new LinearLayout(this);
                var btnParams = new LinearLayout.LayoutParams(width, height);
#if DEBUG
                var str = string.Format("Xdpi:{0} px:{1} {2} {3}", Resources.DisplayMetrics.Xdpi, Resources.DisplayMetrics.WidthPixels, width, height);
#endif
                var n = i * num;
                var g = DeciveList.Count - n;
                var count = g > num ? num : g;
                for (var j = 0; j < count; j++)
                {
                    var device = DeciveList[n + j];
                    var btn = new Button(this) { Text = device.Name, LayoutParameters = btnParams, };
                    btnParams.SetMargins(5, 5, 5, 5);
                    btn.SetBackgroundResource(device.IsFind ? Resource.Drawable.selectedTab : Resource.Drawable.deviceOffline);
                    btn.SetTextColor(Color.White);
                    btn.SetPadding(0, 12, 0, 12);
                    //btn.SetCompoundDrawablesWithIntrinsicBounds(i == 0 ? Resource.Drawable.icon_gateway : 0, 0, i == 1 ? Resource.Drawable.icon_gateway : 0, 0);
                    btn.SetCompoundDrawablesWithIntrinsicBounds(0, device.Icon, 0, 0);
                    btn.Click += (s, e) =>
                    {
                        var sender = s as Button;
#if DEBUG
                        Toast.MakeText(this, str + "  " + sender.Text, ToastLength.Short).Show();
#endif
                    };
                    linearLayout.AddView(btn);
                    var k = rows - 1;
                    var l = count - 1;
                    if (i == k && j == l)
                    {
                        if (!isok)
                        {
                            linearLayout.AddView(CreateFindButton(width, height));
                        }
                    }
                }
                layout.AddView(linearLayout);
            }
            if (isok)
            {
                var linearLayout = new LinearLayout(this);
                linearLayout.AddView(CreateFindButton(width, height));
                layout.AddView(linearLayout);
            }
        }

        /// <summary>创建发现设备按钮</summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        private Button CreateFindButton(Int32 width, Int32 height)
        {
            var btnParams = new LinearLayout.LayoutParams(width, height);
            var btn = new Button(this) { Text = "发现设备", LayoutParameters = btnParams, };
            btnParams.SetMargins(5, 5, 5, 5);
            btn.SetBackgroundResource(Resource.Drawable.selectedTab);
            btn.SetTextColor(Color.White);
            btn.SetPadding(0, 12, 0, 12);
            btn.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.icon_search_white, 0, 0);
            btn.Click += (s, e) =>
            {
                var sender = s as Button;
                Toast.MakeText(this, "发现设备" + sender.Text, ToastLength.Short).Show();
            };
            return btn;
        }


    }
}