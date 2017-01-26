using System;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;

namespace Safe
{
    public class MainPage : ContentPage
    {

        SettingsPage settings_page;
        NavigationPage nav_page;
        SKCanvasView activityview;
        SKCanvasView settingsview;

        public MainPage()
        {

            settings_page = new SettingsPage();
           
            nav_page = new NavigationPage(this);
            NavigationPage.SetHasNavigationBar(this, false);

            var tapGestureRecognizer = new TapGestureRecognizer();
            var tapGestureRecognizer_settings = new TapGestureRecognizer();

            activityview = new SKCanvasView();
            settingsview = new SKCanvasView();

            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            tapGestureRecognizer_settings.Tapped += TapGestureRecognizer_settings_Tapped;
            tapGestureRecognizer.NumberOfTapsRequired = 2; 

            activityview.PaintSurface += View_PaintSurface;
            settingsview.PaintSurface += Settingsview_PaintSurface;

            activityview.GestureRecognizers.Add(tapGestureRecognizer);
            settingsview.GestureRecognizers.Add(tapGestureRecognizer_settings);

            
            int uheigth = (int) (Height / 16);
            int height = (int) Height;
            int width =  (int) Width;

            AbsoluteLayout l = new AbsoluteLayout();
            
            AbsoluteLayout.SetLayoutBounds(activityview, new Rectangle(0, 0, 1, 0.75));
            AbsoluteLayout.SetLayoutFlags(activityview,AbsoluteLayoutFlags.All);
            
            AbsoluteLayout.SetLayoutBounds(settingsview, new Rectangle(0, 1, 1, 0.25));
            AbsoluteLayout.SetLayoutFlags(settingsview, AbsoluteLayoutFlags.All);

            l.Children.Add(activityview);
            l.Children.Add(settingsview);

            Content = l;
        }

        private void StartAnimation()
        {
            Task.Factory.StartNew(
              () => Animation(),
              CancellationToken.None,
              TaskCreationOptions.None,
              TaskScheduler.FromCurrentSynchronizationContext()
              );
        }

        private async Task Animation()
        {
            while (true)
            {
                activityview.InvalidateSurface(); //Repaints the surface each second
                await Task.Delay(1000);
            }
        }

        private void TapGestureRecognizer_settings_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(settings_page);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           //Start the animation
        }

        //w 8 divisions, h 4 divisions
        private void Settingsview_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            int width, heigth, uheigth, uwidth;

            width = e.Info.Width;
            heigth = e.Info.Height;
            uheigth = heigth / 4;
            uwidth = width / 8;

            using (SKPaint paint = new SKPaint()) {
                canvas.Clear(new SKColor(0xe5, 0xef, 0xff));
                paint.Color = new SKColor(0x28, 0x2c, 0xff);  //0x282cff
                //Lines
                for (int i = 0; i < 3; i++) canvas.DrawLine(uwidth, (uheigth * 3.5f) + i, uwidth * 7, (uheigth * 3.5f) + i, paint);
                for (int i = 0; i < 3; i++) canvas.DrawLine(uwidth, (uheigth * 1.5f) + i, uwidth * 7, (uheigth * 1.5f) + i, paint);
                //Texts
                paint.TextSize = uheigth;
                paint.Color = new SKColor(0x00, 0x00, 0x00);
                canvas.DrawText("Settings", uwidth, uheigth * 3, paint);
                paint.TextSize = (int)(0.7 * uheigth);
                canvas.DrawText("Gps... Accelerometer...", uwidth, uheigth * 1, paint);
                displayLayout(canvas, paint, uheigth, uwidth);
            }

        }
        //w 8 divisions, h 12 divisions
        private void View_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            int width, heigth, uheigth, uwidth;

            width = e.Info.Width;
            heigth = e.Info.Height;
            uheigth = heigth / 14;
            uwidth = width / 8;


            int middle_width = e.Info.Width / 2;
            int middle_heigth = e.Info.Height / 2;
            int radius = 3 * uwidth;

            /*
            double dotx = middle_width + radius * Math.Cos(current_angle); //error, the dot is always in the centre
            double doty = middle_heigth + radius * Math.Sin(current_angle);
            */

            using (SKPaint paint = new SKPaint())
            {
                canvas.Clear(new SKColor(0xe5, 0xef, 0xff));
                paint.Color = new SKColor(0x28, 0x2c, 0xff);
                //Circle
                canvas.DrawCircle(middle_width, 5 * uheigth , radius + 1, paint);
                canvas.DrawCircle(middle_width, 5 * uheigth, radius + 2, paint);
                paint.Color = new SKColor(0xe5, 0xef, 0xff);
                canvas.DrawCircle(middle_width, 5 * uheigth, radius, paint);
                paint.Color = new SKColor(0x00, 0x00, 0x00);
                displayLayout(canvas, paint, uheigth, uwidth);
            }
        }

        private void displayLayout(SKCanvas canvas, SKPaint paint, int uheigth, int uwidth )
        {
            for (int i = 0; i< 8; i++)
            {
                canvas.DrawLine(uwidth*i,0,uwidth*i,uheigth*16,paint); //Vertical
            }
            for (int i = 0; i < 14 ; i++)
            {
                canvas.DrawLine(0,uheigth*i,uwidth*8, uheigth * i, paint); //horizontal
            }
        }
    }
}

