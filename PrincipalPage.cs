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
        public object RowDefinitions { get; private set; }

        public MainPage()
        {
            var top = new Button
            {
                Image = "first.png",
                Text = "Danger",
                BackgroundColor = new Color(255, 0, 0),
            };

            var middle = new Button
            {
                Text = "Calibrate",
                BackgroundColor = new Color(0, 255, 0),
                Image = "second.png"
            };

            var bottom = new Button
            {
                Text = "Settings",
                BackgroundColor = new Color(0, 0, 255),
                Image = "third.png"
            };
            
            addEventHandlers(top, middle, bottom);

            var layout = new Grid();

            layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength('*', GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength('*', GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength('*', GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength('*', GridUnitType.Star) });

            layout.Children.Add(top, 0, 0);
            layout.Children.Add(middle, 0, 1);
            layout.Children.Add(bottom, 0, 2);


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
