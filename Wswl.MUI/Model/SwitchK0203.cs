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
    [Serializable]
    public class SwitchK0203 :Java.Lang.Object, ISwitch
    {
        public string Name { get; set; }

        public DeviceType Type { get; set; }

        public List<SwitchPosition> Positions { get; set; }

        public int Count { get; set; }

        public SwitchK0203()
        {
            Name = "K0203";
            Type = DeviceType.K0203;
            Count = 3;
            Positions = new List<SwitchPosition>
            {
                new SwitchPosition{Name = "一号开关",IconOff = Resource.Drawable.icon_device_off,IconOn = Resource.Drawable.icon_device,State = 0},
                new SwitchPosition{Name = "二号开关",IconOff = Resource.Drawable.icon_device_off,IconOn = Resource.Drawable.icon_device,State = 0},
                new SwitchPosition{Name = "三号开关",IconOff = Resource.Drawable.icon_device_off,IconOn = Resource.Drawable.icon_device,State = 1},
            };
        }
    }
}