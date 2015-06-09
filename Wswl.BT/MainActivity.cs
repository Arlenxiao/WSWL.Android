using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Wswl.BT
{
    [Activity(Label = "无声物联蓝牙版", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        #region 属性

        #endregion
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
        }
    }
}

