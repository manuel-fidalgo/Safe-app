using System;
using System.Resources;
using Xamarin.Forms;


namespace Safe
{
    public class MainPage : ContentPage
    {

        //Childrens pages
        AccelerometerPage accelerometer_page;
        DangerActivityPage danger_page;
        SettingsPage settings_page;

        //Navigation page
        NavigationPage nav_page;

        public MainPage()
        {
            
            accelerometer_page = new AccelerometerPage();
            danger_page = new DangerActivityPage();
            settings_page = new SettingsPage();


            nav_page = new NavigationPage(this);
            NavigationPage.SetHasNavigationBar(this, false);


            var top = new Button
            {
                Text = AppResources.danger_activity,
                Image = "first.png", 
                TextColor = Color.Black,
                BackgroundColor = Color.FromRgb(51,181,229), //new Color(r,g,b) is not valid
            };

            var middle = new Button
            {
                Text = AppResources.accelerometer,
                Image = "second.png",
                TextColor = Color.Black,
                BackgroundColor = Color.FromRgb(51, 181, 229),
            };

            var bottom = new Button
            {
                Text = AppResources.settings,
                Image = "third.png",
                TextColor = Color.Black,
                BackgroundColor = Color.FromRgb(51, 181, 229),
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
        
        //Clicked events, push the needed page to the first position.
        private void TopClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(danger_page);
        }

        private void MiddleClicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(accelerometer_page);
        }

        private void BottomClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(settings_page);
        }
    }
}
