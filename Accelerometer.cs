using System;
using Xamarin.Forms;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System.Collections.Generic;

namespace Safe
{
    /**This class will wrap the accelerometeter process, implements the ISensorEventListener interface*/
    class Accelerometer 
    {
        labelrender acceleromer_label;
        List<accelerometerValue> accelerometer_data;
        static readonly int MAX_VALUES = 500;

        static bool ON = true;
        int counter;

        public Accelerometer(labelrender lbl, List<accelerometerValue> data)
        {
            acceleromer_label = lbl;
            accelerometer_data = data;
            if (ON) startAccelerometer();
            counter = 0;
        }

        //Set up
        private void startAccelerometer()
        {
            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer);
            CrossDeviceMotion.Current.SensorValueChanged += Current_SensorValueChanged;        
        }

        //Event handler for sensor value changed
        private void Current_SensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
                try
                {
                    if (e.SensorType == MotionSensorType.Accelerometer)
                    {
                        acceleromer_label.Text = string.Format("x[{0:N3}] y[{1:N3}] z[{2:N3}]", ((MotionVector)e.Value).X, ((MotionVector)e.Value).Y, ((MotionVector)e.Value).Z);
                        if (counter ==  MAX_VALUES)
                        {
                            accelerometer_data.RemoveAt(0);
                            counter--;
                        }
                        accelerometer_data.Add(new accelerometerValue(((MotionVector)e.Value).X, ((MotionVector)e.Value).Y, ((MotionVector)e.Value).Z));
                        counter++;
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Accelerometer exception");
                }
        }
    }
}

