using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;


namespace Safe
{
    class AccelerometerPage : ContentPage
    {
        Accelerometer acceler;
        Gps gps;

        public AccelerometerPage()
        {
            acceler = new Accelerometer();
            gps = new Gps();

            var canvas_view = new SKCanvasView();
            canvas_view.PaintSurface += Canvas_view_PaintSurface;

            Content = new ContentView {
                    Content = canvas_view
            };
        }

        private void Canvas_view_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
           int surfaceWidth = e.Info.Width;
           int surfaceHeight = e.Info.Height;
           SKCanvas canvas = e.Surface.Canvas;
           float side = Math.Min(surfaceHeight, surfaceWidth) * 0.5f;

           using (SKPaint paint = new SKPaint())
           {
               canvas.Clear(Xamarin.Forms.Color.Black.ToSKColor()); //paint it black
               SKRect r1 = new SKRect(10f, 20f, side, side);
               paint.Color = Xamarin.Forms.Color.Blue.ToSKColor();
               canvas.DrawRect(r1, paint);

               paint.Color = Xamarin.Forms.Color.Red.ToSKColor();
               canvas.DrawOval(r1, paint);
           }
        }
    }
}
