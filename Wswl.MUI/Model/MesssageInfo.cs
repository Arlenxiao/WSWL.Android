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
        /// <summary>����</summary>
        public String Title { get; set; }

        /// <summary>����</summary>
        public String Info { get; set; }

        /// <summary>ͼ��</summary>
        public Int32 IconWarning { get; set; }

        /// <summary>�Ƿ��Ķ�</summary>
        public Boolean IsRead { get; set; }
    }
}