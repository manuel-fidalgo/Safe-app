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

        static bool ON = true;

        public Accelerometer(Label lbl)
        {
            acceleromer_label = lbl;
            if (ON) startAccelerometer();
        }

        private void startAccelerometer()
        {
            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer);
            CrossDeviceMotion.Current.SensorValueChanged += Current_SensorValueChanged;        
        }

        private void Current_SensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
                try
                {
                    if (e.SensorType == MotionSensorType.Accelerometer)
                        acceleromer_label.Text = string.Format("x[{0:N3}]\n y[{1:N3}]\n z[{2:N3}]", ((MotionVector)e.Value).X, ((MotionVector)e.Value).Y, ((MotionVector)e.Value).Z);

                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Accelerometer exception");
                }
        }
    }
}

