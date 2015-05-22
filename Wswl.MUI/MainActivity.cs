using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace Wswl.MUI
{
    [Activity(Label = "主界面", Theme = "@style/WswlAppTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : TabActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(WswlVariable.AppTheme);
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //初始化选项卡
            InitTab();
        }

        /// <summary>初始化选项卡</summary>
        private void InitTab()
        {
            CreateTab(typeof(HomeActivity), "HomeActivity", "主页", Resource.Drawable.tab_icon_home);
            CreateTab(typeof(NewsActivity), "NewsActivity", "信息", Resource.Drawable.icon_news_white);
            CreateTab(typeof(DevicesActivity), "DevicesActivity", "设备", Resource.Drawable.icon_devices_white);
            CreateTab(typeof(ManagesActivity), "ManagesActivity", "更多", Resource.Drawable.icon_more_white);

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

            //默认
            //var drawableIcon = Resources.GetDrawable(drawableId);
            //spec.SetIndicator(label, drawableIcon);

            var view = LayoutInflater.Inflate(Resource.Layout.TabStyle, null);
            view.FindViewById<ImageView>(Resource.Id.tabIcon).SetImageResource(drawableId);
            view.FindViewById<TextView>(Resource.Id.tabText).Text = label;

            spec.SetIndicator(view);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }

        /// <summary>选择当前Tab项</summary>
        /// <param name="tag"></param>
        public void SetCurrentTab(string tag)
        {
            TabHost.SetCurrentTabByTag(tag);
        }

    }
}

