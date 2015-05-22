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
    [Activity(Label = "智能继电器")]
    public class K0221Activity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(WswlVariable.AppTheme);
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.K0221);

            //创建开关
            this.CreateSwitch(Resources.DisplayMetrics, Resource.Id.k0221_layout_content, new SwitchK0221());
        }
    }
}