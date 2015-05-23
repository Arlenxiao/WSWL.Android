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

namespace Wswl.MUI
{
    public class WswlTheme
    {
        /// <summary>默认主题</summary>
        public const Int32 DEFAULT = Resource.Style.WswlAppTheme;
        /// <summary>黑色</summary>
        public const Int32 BLACK = Resource.Style.WswlAppThemeBlack;
        /// <summary>绿色</summary>
        public const Int32 GREEN = Resource.Style.WswlAppThemeGreen;
        /// <summary>粉色</summary>
        public const Int32 PINK = Resource.Style.WswlAppThemePink;
    }
}