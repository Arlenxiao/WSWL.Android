using System;
using System.Collections.Generic;
using System.Text;

namespace Wswl.MUI.Model
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceType
    {
        /// <summary>网关</summary>
        W0103 = 0x0103,

        /// <summary>触摸开关</summary>
        K0203 = 0x0203,

        /// <summary>智能继电器</summary>
        K0221 = 0x0221,

        /// <summary>环境探测器</summary>
        G0311 = 0x0311,

        /// <summary>智能门锁</summary>
        J0411 = 0x0411,

        /// <summary>门窗磁</summary>
        A0501 = 0x0501,

        /// <summary>红外感应器</summary>
        A0541 = 0x0541,

        /// <summary>调色控制器</summary>
        C0611 = 0x0611
    }
}
