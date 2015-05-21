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
    /// ���ؽӿ�
    /// </summary>
    public interface ISwitch
    {
        /// <summary>��������</summary>
        String Name { get; set; }

        /// <summary>��������</summary>
        DeviceType Type { get; set; }

        /// <summary>����λ����</summary>
        List<SwitchPosition> Positions { get; set; }

        /// <summary>����λ����</summary>
        Int32 Count { get; set; }

    }
}