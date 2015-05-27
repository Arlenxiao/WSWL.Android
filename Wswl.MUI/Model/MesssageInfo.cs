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

namespace Wswl.MUI.Model
{
    public class MessageInfo
    {
        /// <summary>标题</summary>
        public String Title { get; set; }

        /// <summary>内容</summary>
        public String Info { get; set; }

        /// <summary>图标</summary>
        public Int32 IconWarning { get; set; }

        /// <summary>是否阅读</summary>
        public Boolean IsRead { get; set; }
    }
}