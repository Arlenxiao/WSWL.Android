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
using Wswl.MUI.Model;

namespace Wswl.MUI
{
    [Activity(Label = "触摸开关")]
    public class K0203Activity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(WswlVariable.AppTheme);
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.K0203);

            //创建开关
            this.CreateSwitch(Resources.DisplayMetrics, Resource.Id.k0203_layout_content, new SwitchK0203());
        }
    }
}