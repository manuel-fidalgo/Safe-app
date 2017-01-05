using System;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Xamarin.Forms;
using Android.Content;

namespace Safe
{
    /**This class will wrap the accelerometeter process, implements the ISensorEventListener interface*/
    class Accelerometer 
    {
        Label acceleromer_label;

        public Accelerometer(Label lbl)
        {
         //   _sensorManager = GetSystemService();
            acceleromer_label = lbl;
            acceleromer_label.Text = "Not implemented yet";
        }
        
        /*
        protected override void OnResume()
        {
           base.OnResume();
           _sensorManager.RegisterListener(this,
                                           _sensorManager.GetDefaultSensor(SensorType.Accelerometer),
                                           SensorDelay.Ui);
        }
        protected override void OnPause()
        {
           base.OnPause();
           _sensorManager.UnregisterListener(this);
        }
        */

    }
}

