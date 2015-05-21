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
    public class SwitchK0221:ISwitch
    {
        public string Name { get; set; }

        public DeviceType Type { get; set; }

        public List<SwitchPosition> Positions { get; set; }

        public int Count { get; set; }

         public SwitchK0221()
        {
            Name = "K0221";
            Type = DeviceType.K0221;
            Count = 1;
            Positions=new List<SwitchPosition>
            {
                new SwitchPosition{Name = "“ª∫≈Œª",IconOff = Resource.Drawable.icon_device_off,IconOn = Resource.Drawable.icon_device},
            };
        }
    }
}