using System;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Reflection;

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

            top_view = new CustomView(AppResources.danger_activity,"first.png");
            var top_tap = new TapGestureRecognizer();
            top_tap.Tapped += TopTapped;
            top_view.GestureRecognizers.Add(top_tap);

            middle_view = new CustomView(AppResources.hardware_page_tittle,"second.png");
            var middle_tap = new TapGestureRecognizer();
            middle_tap.Tapped += MiddleTapped;
            middle_view.GestureRecognizers.Add(middle_tap);

            bottom_view = new CustomView(AppResources.settings,"third.png");
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
        String path;
        static readonly int H_DIV = 12;
        static readonly int W_DIV = 16; //Heigth And width divisions

        public CustomView(String text, String resource_path)
        {
            view_text = text;
            path = resource_path;
            SKCanvasView canvas_view = new SKCanvasView();
            canvas_view.PaintSurface += PaintSurface;
            Content = canvas_view;

        }

        public void PaintSurface(object sendes, SKPaintSurfaceEventArgs e)
        {
            int h_unity, w_unity;

            SKCanvas myCanvas = e.Surface.Canvas;
            int surfaceWidth = e.Info.Width;
            int surfaceHeight = e.Info.Height;

            w_unity = surfaceWidth / W_DIV;
            h_unity = surfaceHeight / H_DIV;


            myCanvas.Clear();

            using (var paint = new SKPaint())
            {


                paint.IsAntialias = true;
                paint.Color = new SKColor(0xe5, 0xef, 0xff);
                //Background
                paint.StrokeCap = SKStrokeCap.Round;
                myCanvas.DrawRect(new SKRect(0, 0, surfaceWidth, surfaceHeight), paint);
                //Line
                paint.Color = new SKColor(0x28, 0x2c, 0xff);  //0x282cff
                for (int i = 0; i < 3; i++) myCanvas.DrawLine(w_unity, (h_unity * 4) + i, w_unity * 15, (h_unity * 4) + i, paint);

                //Tittle
                paint.Color = new SKColor(0x00, 0x00, 0x00);
                paint.TextSize = 24 * 3;
                myCanvas.DrawText(view_text, w_unity * 2, h_unity * 3, paint);

                //Image
                try
                {
                    var assembly = typeof(CustomView).GetTypeInfo().Assembly;
                    using (var resource = assembly.GetManifestResourceStream(path))
                    using (var stream = new SKManagedStream(resource))
                    using (var bitmap = SKBitmap.Decode(stream))
                    {
                        myCanvas.DrawBitmap(bitmap, new SKRect(2 * w_unity, 5 * h_unity, 6 * w_unity, 11 * h_unity), paint);
                    }
                }
                catch (Exception ) { }
            }
        }
    }
}

