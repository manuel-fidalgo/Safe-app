﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Safe
{
    class AccelerometerPage : ContentPage
    {
        Accelerometer acceler;
       
        public AccelerometerPage()
        {
            acceler = new Accelerometer();
        }
    }
}
