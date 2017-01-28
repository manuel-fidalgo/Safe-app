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
        SKColor TEXT, BALL, GRADIENT0, GRADIENT1;


        public MainPage()
        {
            initColors();
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


            int uheigth = (int)(Height / 16);
            int height = (int)Height;
            int width = (int)Width;

            AbsoluteLayout l = new AbsoluteLayout();

            AbsoluteLayout.SetLayoutBounds(activityview, new Rectangle(0, 0, 1, 0.75));
            AbsoluteLayout.SetLayoutFlags(activityview, AbsoluteLayoutFlags.All);

            AbsoluteLayout.SetLayoutBounds(settingsview, new Rectangle(0, 1, 1, 0.25));
            AbsoluteLayout.SetLayoutFlags(settingsview, AbsoluteLayoutFlags.All);

            l.Children.Add(activityview);
            l.Children.Add(settingsview);

            Content = l;
        }

        private void initColors()
        {
            BALL = new SKColor(255,0,0);
            TEXT = new SKColor(165, 167, 159);
            GRADIENT0 = new SKColor(110,110,110);
            GRADIENT1 = new SKColor(37, 40, 42);
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
            while (animation_runing)
            {
                if (angle >= 360)
                {
                    angle = 0;
                    await DisplayAlert("Alert", "Are you ok?", "OK");
                }
                else
                {
                    angle++; //Update the angle
                }
                activityview.InvalidateSurface(); //Repaints the surface each second
                await Task.Delay(MAX_MILISECONDS / 360); //One cycle for each 3 seconds
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
                angle = 0;
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

            using (SKPaint paint = new SKPaint())
            {
                paint.IsAntialias = true;

                var shader = SKShader.CreateColor(GRADIENT1);
                paint.Shader = shader;
                canvas.DrawPaint(paint);

                //Line
                paint.Shader = SKShader.CreateColor(TEXT);
                for (int i = 0; i < 3; i++) canvas.DrawLine(uwidth, 0 + i, uwidth * 7, 0 + i, paint);
                //Settings box
                paint.Shader = SKShader.CreateColor(BALL);
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = 5;      //Bigger wrapper                
                canvas.DrawRoundRect(new SKRect(2 * uwidth, 1 * uheigth, 6 * uwidth,3 * uheigth), 20, 20, paint);
                paint.StrokeWidth = 1;
                //Texts
                paint.Style = SKPaintStyle.Fill;
                paint.TextSize = uheigth;
                paint.TextAlign = SKTextAlign.Center;
                paint.Shader = SKShader.CreateColor(TEXT);
                canvas.DrawText("Settings", width / 2,(int)(uheigth * 2.5), paint);
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
                canvas.Clear();
                var colors = new SKColor[] { GRADIENT0,GRADIENT1 };

                var shader = SKShader.CreateRadialGradient(new SKPoint(middle_width,middle_width),radius,colors,null,SKShaderTileMode.Clamp) ;
                paint.Shader = shader;
                canvas.DrawPaint(paint);
            
                //Circle

                paint.Shader = SKShader.CreateColor(TEXT);
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = 5;
                canvas.DrawCircle(middle_width, middle_heigth, radius , paint);
                paint.StrokeWidth = 1;

                paint.Style = SKPaintStyle.StrokeAndFill; 
                
                //Text tap twice
                paint.TextAlign = SKTextAlign.Center;
                paint.TextSize = uheigth;
                if (animation_runing)
                    canvas.DrawText("Tap twice", middle_width, 12 * uheigth, paint);
                else
                    canvas.DrawText("Tap twice to start", middle_width, 12 * uheigth, paint);

                //External dot
                double angle_rad = angle * 0.0174533;
                if (animation_runing)
                {

                    double x = middle_width + radius * Math.Cos(angle_rad);
                    double y = middle_heigth + radius * Math.Sin(angle_rad);

                    if (angle > 270)
                    {
                        paint.TextSize = 2 * uheigth;
                        paint.TextAlign = SKTextAlign.Center;
                        paint.Shader = SKShader.CreateColor(BALL);
                        canvas.DrawText("CLICK!", middle_width, (int)(uheigth * 5.5), paint);
                    }
                    paint.Shader = SKShader.CreateColor(BALL);
                    canvas.DrawCircle((int)x, (int)y, 15, paint);
                }
            }
        }

        private void displayLayout(SKCanvas canvas, SKPaint paint, int uheigth, int uwidth)
        {
            for (int i = 0; i < 8; i++)
            {
                canvas.DrawLine(uwidth * i, 0, uwidth * i, uheigth * 16, paint); //Vertical
            }
            for (int i = 0; i < 14; i++)
            {
                canvas.DrawLine(0, uheigth * i, uwidth * 8, uheigth * i, paint); //horizontal
            }
        }
    }
}

