using System;
using Xamarin.Forms;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;

namespace Safe
{
    /**This class will wrap the accelerometeter process, implements the ISensorEventListener interface*/
    class Accelerometer 
    {
        Label acceleromer_label;

        public Accelerometer(Label lbl)
        {
            acceleromer_label = lbl;
            startAccelerometer();
        }

        private void startAccelerometer()
        {
            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer);
            CrossDeviceMotion.Current.SensorValueChanged += Current_SensorValueChanged;        
        }

        private void Current_SensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            if (e.SensorType == MotionSensorType.Accelerometer)
            {
                acceleromer_label.Text = string.Format("x[{0}] y[{1}] z[{2}]", ((MotionVector)e.Value).X, ((MotionVector)e.Value).Y, ((MotionVector)e.Value).Z);
            }
        }
    }
}

