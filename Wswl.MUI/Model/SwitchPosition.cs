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
    /// <summary>
    /// 开关位
    /// </summary>
    [Serializable]
    public class SwitchPosition : Java.Lang.Object
    {
        /// <summary>开关位名称</summary>
        public String Name { get; set; }

        /// <summary>开 对应资源图片地址</summary>
        public Int32 IconOn { get; set; }

        /// <summary>关 对应资源图片地址</summary>
        public Int32 IconOff { get; set; }

        /// <summary>
        /// 开关状态 0:关  1:开
        /// </summary>
        public Int32 State { get; set; }
    }
}