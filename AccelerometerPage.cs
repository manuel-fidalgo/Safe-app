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
        StackLayout layout;
        public MainPage mainpage; //Contencion mutua para poder pasar de una a otra facilmente
  
        public AccelerometerPage()
        {
            var exit_button = new Button
            {
                Text = "Exit",
                TextColor = Color.Black
            };
            exit_button.Clicked += exit_button_event;


            acceler = new Accelerometer();
            layout = new StackLayout();

            layout.Children.Add(acceler.getLabel());
            layout.Children.Add(exit_button);
            Content = layout;
        }

        private void exit_button_event(object sender, EventArgs e)
        {
            this.IsVisible = false;
            mainpage.IsVisible = true;
        }
    }
}
