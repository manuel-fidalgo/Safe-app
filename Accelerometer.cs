using Android.Hardware;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;
using CoreMotion;

namespace Safe
{
    /**This class will wrap the accelerometeter process, implements the ISensorEventListener interface*/
    class Accelerometer  
    {
        Label label;

        public Accelerometer()
        {
            label = new Label();
            label.Text = "Null";
        }
        public Label getLabel()
        {
            return label;
        }
    }
}
