﻿using System;
using Android.Hardware;
using Android.OS;
using Android.Widget;
using Xamarin.Forms;

namespace Safe
{
    /**This class will wrap the accelerometeter process, implements the ISensorEventListener interface*/
    class Accelerometer
    {
        Label label;
        public Accelerometer()
        {
            label = new Label
            {
                Text = "Not implemented yet"
            };
        } 
        public Label getLabel()
        {
            return label;
        }
    }
}
