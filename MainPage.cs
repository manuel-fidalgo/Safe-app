using System;
using System.Resources;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views;
using SkiaSharp.Views.Forms;


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

            //CUSTONVIEWS

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
        String view_text;

        public CustomView(String text)
        {
            view_text = text;
            SKCanvasView canvas_view = new SKCanvasView();
            canvas_view.PaintSurface += PaintSurface;
            Content = canvas_view;

        }

        public void PaintSurface(object sendes, SKPaintSurfaceEventArgs e)
        {
            SKCanvas myCanvas = e.Surface.Canvas;
            int surfaceWidth = e.Info.Width;
            int surfaceHeight = e.Info.Height;

            myCanvas.Clear();
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = new SKColor(0xe5efff);
                paint.StrokeCap = SKStrokeCap.Round;
                myCanvas.DrawRect(new SKRect(0,0,surfaceWidth,surfaceHeight),paint);
                paint.Color = new SKColor(0x282cff);  //0x282cff
                myCanvas.DrawLine(surfaceWidth/8,surfaceHeight/6, (surfaceWidth / 8)*7, surfaceHeight / 5, paint);
                paint.Color = new SKColor(0xffffff);
                myCanvas.DrawText(view_text,surfaceHeight/12, surfaceHeight / 12, paint);
            }
        }
    }
}

