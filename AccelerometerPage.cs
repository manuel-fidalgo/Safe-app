using System;
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
       
            var exit_button = new Button
            {
                Text = "Exit",
                TextColor = Color.Black,
                BackgroundColor = Color.FromRgb(100,100,100)
            };
            exit_button.Clicked += exit_button_event;

            Content = new StackLayout
            {
                Padding = 20,
                Children =
                {

                    exit_button,

                }
            };
        }

        private void exit_button_event(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
