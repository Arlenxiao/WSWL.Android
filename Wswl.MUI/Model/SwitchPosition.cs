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
    /// ����λ
    /// </summary>
    [Serializable]
    public class SwitchPosition : Java.Lang.Object
    {
        /// <summary>����λ����</summary>
        public String Name { get; set; }

        /// <summary>�� ��Ӧ��ԴͼƬ��ַ</summary>
        public Int32 IconOn { get; set; }

        /// <summary>�� ��Ӧ��ԴͼƬ��ַ</summary>
        public Int32 IconOff { get; set; }

        /// <summary>
        /// ����״̬ 0:��  1:��
        /// </summary>
        public Int32 State { get; set; }
    }
}