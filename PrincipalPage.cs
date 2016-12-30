using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Safe
{
    public class MainPage : ContentPage
    {
   
        public MainPage()
        {
            var top = new Button
            {
                Text = "top",
                BackgroundColor = new Color(127, 0, 0)
            };

            var middle = new Button
            {
                Text = "middle",
                BackgroundColor = new Color(0, 127, 0)
            };

            var bottom = new Button
            {
                Text = "Bottom",
                BackgroundColor = new Color(0, 0, 127)
            };

            addEventHandlers(top, middle, bottom);

            var layout = new Grid
            {
                
                Padding = 40,
                Children = { top, middle, bottom }
            };

            Content = layout;
            
        }

        private void addEventHandlers(Button top, Button middle, Button bottom)
        {
            top.Clicked += TopClicked;
            middle.Clicked += MiddleClicked;
            bottom.Clicked += BottomClicked;
        }
        
        //Clicked events
        private void TopClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info Message", "top", "Ok");
        }

        private void MiddleClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info Message", "middle", "Ok");
        }

        private void BottomClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info Message", "bottom", "Ok");
        }
    }
}
