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

        static readonly int MAX_MILISECONDS = 3000;

        SettingsPage settings_page;
        NavigationPage nav_page;
        SKCanvasView activityview;
        SKCanvasView settingsview;
        int angle;
        bool animation_runing;


        public MainPage()
        {
            angle = 0;

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
        private void FinishAnimation()
        {
            animation_runing = false;
        }

        private async Task Animation()
        {
            animation_runing = true;
            while (animation_runing) {
                if (angle >= 360) angle = 0; else angle++; //Update the angle
                activityview.InvalidateSurface(); //Repaints the surface each second
                await Task.Delay(MAX_MILISECONDS/360); //One cycle for each 3 seconds
            }
        }

        private void TapGestureRecognizer_settings_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(settings_page);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!animation_runing)
                StartAnimation();
            else
                FinishAnimation();
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
                //displayLayout(canvas, paint, uheigth, uwidth);
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
            int middle_heigth = 5 * uheigth;
            int radius = 3 * uwidth;

            using (SKPaint paint = new SKPaint())
            {
                canvas.Clear(new SKColor(0xe5, 0xef, 0xff));
                paint.Color = new SKColor(0x28, 0x2c, 0xff);

                //Circle
                canvas.DrawCircle(middle_width, middle_heigth, radius + 1, paint);
                canvas.DrawCircle(middle_width, middle_heigth, radius + 2, paint);
                paint.Color = new SKColor(0xe5, 0xef, 0xff);
                canvas.DrawCircle(middle_width, middle_heigth, radius, paint);
                paint.Color = new SKColor(0x00, 0x00, 0x00);
                //displayLayout(canvas, paint, uheigth, uwidth);

                //Text tap twice
                paint.TextAlign = SKTextAlign.Center;
                paint.TextSize = uheigth;
                canvas.DrawText("Tap twice to start",middle_width,12*uheigth,paint);

                //External dot
                double angle_rad = angle * 0.0174533;
                if (animation_runing)
                {
                    double x, y;
                    if (angle < 90)
                    {
                        x = middle_width + radius * Math.Cos(angle_rad);
                        y = middle_heigth + radius * Math.Sin(angle_rad);
                    }
                    else if (angle < 180)
                    {
                        x = middle_width + radius * Math.Cos(angle_rad);
                        y = middle_heigth + radius * Math.Sin(angle_rad);
                    }
                    else if (angle < 270)
                    {
                        x = middle_width + radius * Math.Cos(angle_rad);
                        y = middle_heigth + radius * Math.Sin(angle_rad);
                    }
                    else
                    {
                        paint.TextSize = 2 * uheigth;
                        paint.TextAlign = SKTextAlign.Center;
                        paint.Color = new SKColor(0xFF, 0x00, 0x00);
                        canvas.DrawText("CLICK!", middle_width,(int) (uheigth * 5.5), paint);
                        x = middle_width + radius * Math.Cos(angle_rad);
                        y = middle_heigth + radius * Math.Sin(angle_rad);
                    }
                    paint.Color = new SKColor(0xFF, 0x00, 0x00);
                    canvas.DrawCircle((int)x, (int)y, 15, paint);
                }
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

