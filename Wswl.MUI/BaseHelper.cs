using System;
using System.Collections.Generic;
using System.Text;
using Wswl.MUI.Model;

namespace Wswl.MUI
{
    public class BaseHelper
    {
        /// <summary>获取对应Activity Type</summary>
        /// <param name="type">设备类型</param>
        /// <returns>Type</returns>
        public static Type GetActivityType(Int32 type)
        {

            switch (type)
            {
                case (int)DeviceType.K0221: return typeof(K0221Activity);
                case (int)DeviceType.W0103:
                case (int)DeviceType.K0203:
                case (int)DeviceType.G0311:
                case (int)DeviceType.J0411:
                case (int)DeviceType.A0501:
                case (int)DeviceType.A0541:
                case (int)DeviceType.C0611: return typeof(K0203Activity);
            }
            return typeof(K0203Activity);
        }
    }
}
