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
        
        List<VectorValue> accelerometer_data;
        int buffer_counter;
        int buffer_size;


        public Accelerometer(List<VectorValue> data, int buff)
        {
            buffer_size = buff;
            accelerometer_data = data;
            startAccelerometer();
            buffer_counter = 0;
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
                    if (buffer_counter == buffer_size)
                    {
                        accelerometer_data.RemoveAt(0);
                        buffer_counter--;
                    }
                    accelerometer_data.Add(new VectorValue(((MotionVector)e.Value).X, ((MotionVector)e.Value).Y, ((MotionVector)e.Value).Z));
                    buffer_counter++;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}

