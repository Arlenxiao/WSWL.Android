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
    /// 开关接口
    /// </summary>
    public interface ISwitch
    {
        /// <summary>开关名称</summary>
        String Name { get; set; }

        /// <summary>开关类型</summary>
        DeviceType Type { get; set; }

        /// <summary>开关位集合</summary>
        List<SwitchPosition> Positions { get; set; }

        /// <summary>开关位个数</summary>
        Int32 Count { get; set; }

    }
}