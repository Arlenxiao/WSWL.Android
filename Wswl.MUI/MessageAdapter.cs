using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Wswl.MUI.Model;

namespace Wswl.MUI
{
    public class MessageAdapter : BaseAdapter<MessageInfo>
    {
        private List<MessageInfo> items;

        private Activity content;

        public MessageAdapter(Activity _content, List<MessageInfo> _items)
            : base()
        {
            this.content = _content;
            this.items = _items;
        }

        public override MessageInfo this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            var view = convertView;
            if (view == null) view = content.LayoutInflater.Inflate(Resource.Layout.MessageView, null);

            view.FindViewById<ImageView>(Resource.Id.message_warning).SetImageResource(item.IconWarning);
            view.FindViewById<TextView>(Resource.Id.message_title).Text = item.Title;
            var read = view.FindViewById<TextView>(Resource.Id.message_info);
            read.Text = item.IsRead ? "ÒÑ¶Á" : "Î´¶Á";
            read.SetTextColor(item.IsRead ? Color.Gray : Color.White);
            return view;
        }
    }
   
}