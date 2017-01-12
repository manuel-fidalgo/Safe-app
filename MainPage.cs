using System;
using System.Resources;
using Xamarin.Forms;


namespace Safe
{
    public class MainPage : ContentPage
    {

        //Childrens pages
        HardwarePage hardware_page;
        DangerActivityPage danger_page;
        SettingsPage settings_page;

        //Navigation page
        NavigationPage nav_page;

        CustomView top_view, middle_view, bottom_view;

        public MainPage()
        {
            
            hardware_page = new HardwarePage();
            danger_page = new DangerActivityPage();
            settings_page = new SettingsPage();


            nav_page = new NavigationPage(this);
            NavigationPage.SetHasNavigationBar(this, false);

            /*
            
            //DONE WITH BUTTONS

            var top = new Button
            {
                Text = AppResources.danger_activity,
                Image = "first.png",
                TextColor = Color.Black,
                BackgroundColor = Color.FromRgb(255,255,255), //new Color(r,g,b) is not valid
            };

            var middle = new Button
            {
                Text = AppResources.hardware_page_tittle,
                Image = "second.png",
                TextColor = Color.Black,
                BackgroundColor = Color.FromRgb(255, 255, 255),
            };

            var bottom = new Button
            {
                Text = AppResources.settings,
                Image = "third.png",
                TextColor = Color.Black,
                BackgroundColor = Color.FromRgb(255, 255, 255),
            };

            top.Clicked += TopTapped;
            middle.Clicked += MiddleTapped;
            bottom.Clicked += BottomTapped;
            */


            //DONE WITH CUSTONVIEWS

            top_view = new CustomView(AppResources.danger_activity);
            var top_tap = new TapGestureRecognizer();
            top_tap.Tapped += TopTapped;
            top_view.GestureRecognizers.Add(top_tap);

            middle_view = new CustomView(AppResources.hardware_page_tittle);
            var middle_tap = new TapGestureRecognizer();
            middle_tap.Tapped += MiddleTapped;
            middle_view.GestureRecognizers.Add(middle_tap);

            bottom_view = new CustomView(AppResources.settings);
            var bottom_tap = new TapGestureRecognizer();
            bottom_tap.Tapped += BottomTapped;
            bottom_view.GestureRecognizers.Add(bottom_tap);

            var layout = new Grid();

            layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength('*', GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength('*', GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength('*', GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength('*', GridUnitType.Star) });

            layout.Children.Add(top_view, 0, 0);
            layout.Children.Add(middle_view, 0, 1);
            layout.Children.Add(bottom_view, 0, 2);
            layout.Padding = 0;

            Content = layout;
        }


        //Clicked events, push the needed page to the first position.
        private void TopTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(danger_page);
        }

        private void MiddleTapped(object sender, EventArgs e)
        {
           Navigation.PushAsync(hardware_page);
        }

        private void BottomTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(settings_page);
        }
    }

    internal class CustomView : ContentView
    {
        Color color { get; set; }
        Label txt;
        BoxView line;

        public CustomView(String text)
        {
            txt = new Label
            {
                Text = text,
                TextColor = Color.Lime
            };
            Content = txt;
        }
    }
}

