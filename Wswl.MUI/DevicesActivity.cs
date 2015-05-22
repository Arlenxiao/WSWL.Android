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
                new DeviceInfo{Name = "网关",Icon= Resource.Drawable.icon_gateway,IsFind = true,Type=DeviceType.W0103},
                new DeviceInfo{Name = "喝茶室开关",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "办公区开关",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "大厅红外",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.A0541},
                new DeviceInfo{Name = "前门开关",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "喝茶室门窗磁",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.A0501},
                new DeviceInfo{Name = "智能继电器",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0221},
                new DeviceInfo{Name = "触摸开关",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.K0203},
                new DeviceInfo{Name = "红外感应器",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.A0541},
                new DeviceInfo{Name = "门窗磁",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.A0501},
                new DeviceInfo{Name = "环境探测器",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.G0311},
                new DeviceInfo{Name = "调色控制器",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.C0611},
                new DeviceInfo{Name = "智能门锁",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.J0411},
            };

            DeciveList = DeciveList.OrderByDescending(m => m.IsFind).ToList();
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
            this.CreateDevice(Resources.DisplayMetrics, Resource.Id.deveces_content, DeciveList, true);
        }
    }
}