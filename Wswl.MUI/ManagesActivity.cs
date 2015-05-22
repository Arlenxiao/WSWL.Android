using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Wswl.MUI
{
    [Activity(Label = "…Ë÷√π‹¿Ì", Theme = "@style/WswlAppTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class ManagesActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Manages);

            InitEvent();
        }

        private void InitEvent()
        {
            var system = FindViewById<Button>(Resource.Id.btn_manages_system);
            system.Click += (s, e) => { StartActivity(typeof(SystemActivity)); };
        }
    }
}