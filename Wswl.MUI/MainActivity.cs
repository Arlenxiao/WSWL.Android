using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Wswl.MUI
{
    [Activity(Label = "主界面")]
    public class MainActivity : TabActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //初始化选项卡
            InitTab();
        }

        /// <summary>初始化选项卡</summary>
        private void InitTab()
        {
            CreateTab(typeof(HomeActivity), "主页", "主页", Resource.Drawable.icon_home_white);
            CreateTab(typeof(NewsActivity), "信息", "信息", Resource.Drawable.icon_news_white);
            CreateTab(typeof(DevicesActivity), "设备", "设备", Resource.Drawable.icon_devicesl_white);
            CreateTab(typeof(ManagesActivity), "更多", "更多", Resource.Drawable.icon_more_white);

        }

        /// <summary>创建一个选项卡</summary>
        /// <param name="activityType">选项卡页面</param>
        /// <param name="tag">标签</param>
        /// <param name="label">显示信息</param>
        /// <param name="drawableId">显示图标</param>
        private void CreateTab(Type activityType, string tag, string label, int drawableId)
        {
            var intent = new Intent(this, activityType);
            intent.AddFlags(ActivityFlags.NewTask);

            var spec = TabHost.NewTabSpec(tag);
            var drawableIcon = Resources.GetDrawable(drawableId);
            spec.SetIndicator(label, drawableIcon);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }

    }
}

