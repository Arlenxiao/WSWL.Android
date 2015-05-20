using System;
using System.Collections.Generic;
using System.Text;

namespace Wswl.MUI.Model
{
    public class DeviceInfo
    {
        /// <summary>设备名称</summary>
        public String Name { get; set; }

        /// <summary>设备图标</summary>
        public Int32 Icon { get; set; }

        /// <summary>是否发现设备</summary>
        public Boolean IsFind { get; set; }

        /// <summary>设备类型</summary>
        public DeviceType Type { get; set; }

    }
}
