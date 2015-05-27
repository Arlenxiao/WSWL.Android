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

namespace Wswl.MUI
{
    [Activity(Label = "信息")]
    public class NewsActivity : Activity
    {
        public NewsActivity()
        {
            ListMessages=new List<MessageInfo>();
        }

        private List<MessageInfo> ListMessages { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.News);

            //绑定数据
            for (var i = 0; i < 20; i++)
            {
                ListMessages.Add(new MessageInfo
                {
                    Title = "标题" + i,
                    IconWarning = i % 3 == 0 ? Resource.Drawable.icon_warning : Resource.Drawable.icon_un_warning,
                    Info = "消息",
                    IsRead = i % 2 == 0
                });
            }

            var messages = FindViewById<ListView>(Resource.Id.lv_news_messages);
            messages.Adapter = new MessageAdapter(this, ListMessages);
            messages.ItemClick += (s, e) =>
            {
                var lv = s as ListView;
                var v = ListMessages[e.Position];
                this.ShowMessage(v.Info);
            };
        }
    }
}