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
                new DeviceInfo{Name = "����",Icon= Resource.Drawable.icon_gateway,IsFind = true},
                new DeviceInfo{Name = "�Ȳ��ҿ���",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "�칫������",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "��������",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "ǰ�ſ���",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "�Ȳ����Ŵ���",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "���̵ܼ���",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "��������",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "�����Ӧ��",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "�Ŵ���",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "����̽����",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "��ɫ������",Icon= Resource.Drawable.icon_device,IsFind = true},
                new DeviceInfo{Name = "��������",Icon= Resource.Drawable.icon_device,IsFind = true},
            };
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
            CreateDevice();
        }



        /// <summary>���������豸</summary>
        private void CreateDevice()
        {
            var width = Convert.ToInt32(110 * Resources.DisplayMetrics.Xdpi / 160);
            var height = Convert.ToInt32(80 * Resources.DisplayMetrics.Ydpi / 160);
            var num = Convert.ToInt32(Resources.DisplayMetrics.WidthPixels / width);
            var rows = DeciveList.Count / num + ((DeciveList.Count % num == 0) ? 0 : 1);

            var isok = DeciveList.Count % num == 0;//�Ƿ�պ�һ��

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

        /// <summary>���������豸��ť</summary>
        /// <param name="width">��</param>
        /// <param name="height">��</param>
        /// <returns></returns>
        private Button CreateFindButton(Int32 width, Int32 height)
        {
            var btnParams = new LinearLayout.LayoutParams(width, height);
            var btn = new Button(this) { Text = "�����豸", LayoutParameters = btnParams, };
            btnParams.SetMargins(5, 5, 5, 5);
            btn.SetBackgroundResource(Resource.Drawable.selectedTab);
            btn.SetTextColor(Color.White);
            btn.SetPadding(0, 12, 0, 12);
            btn.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.icon_search_white, 0, 0);
            btn.Click += (s, e) =>
            {
                var sender = s as Button;
                Toast.MakeText(this, "�����豸" + sender.Text, ToastLength.Short).Show();
            };
            return btn;
        }


    }
}