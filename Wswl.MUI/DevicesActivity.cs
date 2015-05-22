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
    [Activity(Label = "�豸�б�")]
    public class DevicesActivity : Activity
    {
        public DevicesActivity()
        {
            DeciveList = new List<DeviceInfo>
            {
                new DeviceInfo{Name = "����",Icon= Resource.Drawable.icon_gateway,IsFind = true,Type=DeviceType.W0103},
                new DeviceInfo{Name = "�Ȳ��ҿ���",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "�칫������",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "��������",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.A0541},
                new DeviceInfo{Name = "ǰ�ſ���",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0203},
                new DeviceInfo{Name = "�Ȳ����Ŵ���",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.A0501},
                new DeviceInfo{Name = "���̵ܼ���",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.K0221},
                new DeviceInfo{Name = "��������",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.K0203},
                new DeviceInfo{Name = "�����Ӧ��",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.A0541},
                new DeviceInfo{Name = "�Ŵ���",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.A0501},
                new DeviceInfo{Name = "����̽����",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.G0311},
                new DeviceInfo{Name = "��ɫ������",Icon= Resource.Drawable.icon_device,IsFind = true,Type=DeviceType.C0611},
                new DeviceInfo{Name = "��������",Icon= Resource.Drawable.icon_device,IsFind = false,Type=DeviceType.J0411},
            };

            DeciveList = DeciveList.OrderByDescending(m => m.IsFind).ToList();
        }

        #region ����

        private List<DeviceInfo> DeciveList { get; set; }

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Devices);

            //���������豸
            this.CreateDevice(Resources.DisplayMetrics, Resource.Id.deveces_content, DeciveList, true);
        }
    }
}