using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;


namespace Safe
{
    class HardwarePage : ContentPage
    {
        Accelerometer acceler;
        Gps gps;

        Label gps_label;
        Label accelerometer_label;
            
        public HardwarePage()
        {
            gps_label = new Label()
            {
                Text = "Gps",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            
            accelerometer_label = new Label()
            {
                Text = "accelerometer",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            acceler = new Accelerometer(accelerometer_label);
            gps = new Gps(gps_label);

            Content = new StackLayout {
                    Children = {
                        gps_label,
                        accelerometer_label
                    }
                    
            };
        }
    }
}
