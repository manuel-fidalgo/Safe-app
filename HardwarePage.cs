using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Threading;

namespace Safe
{
    class HardwarePage : ContentPage
    {
        Accelerometer acceler;
        Gps gps;

        labelrender gps_label;
        labelrender accelerometer_label;

        SKCanvasView view;

        List<accelerometerValue> accelerometer_data;


        public HardwarePage()
        {
            gps_label = new labelrender();
            accelerometer_label = new labelrender();
            gps_label.Text = "Not avaliable";
            accelerometer_label.Text = "Not avaliable";

            accelerometer_data = new List<accelerometerValue>();

            acceler = new Accelerometer(accelerometer_label,accelerometer_data);
            gps = new Gps(gps_label);

            view = new SKCanvasView();
            view.PaintSurface += View_PaintSurface;
            Content = view;

            Task.Factory.StartNew(
                () => this.update(),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext()
                );
        }

        private async Task update()
        {
            while (true)
            {
                view.InvalidateSurface(); //Repaint method
                Gps.getGps().getGpsLocation();
                await Task.Delay(100);
            }
        }
        
        //Event wich is called when the surface should be repainted
        private void View_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            int h_unity, w_unity, H_DIV, W_DIV;

            SKCanvas canvas = e.Surface.Canvas;
            int surfaceWidth = e.Info.Width;
            int surfaceHeight = e.Info.Height;
            H_DIV = 16; W_DIV = 8;

            w_unity = surfaceWidth / W_DIV;
            h_unity = surfaceHeight / H_DIV;

            using (SKPaint paint = new SKPaint())
            {
                paint.IsAntialias = true;
                //Background
                paint.Color = new SKColor(0xe5, 0xef, 0xff);
                canvas.DrawRect(new SKRect(0,0,surfaceWidth,surfaceHeight),paint);
                //Titles
                paint.Color = new SKColor(0x00, 0x00, 0x00);
                paint.TextSize = 70;
                canvas.DrawText("GPS:",w_unity,h_unity*2,paint);
                canvas.DrawText("Accelerometer:", w_unity , h_unity * 10, paint);
                //Data
                paint.TextSize = 35;
                canvas.DrawText(gps_label.Text, 2 * w_unity, 4 * h_unity, paint);
                canvas.DrawText(accelerometer_label.Text, 2 * w_unity, 12 * h_unity, paint);
                //Lines
                paint.Color = new SKColor(0x28, 0x2c, 0xff);  //0x282cff
                for (int i = 0; i < 3; i++) canvas.DrawLine(w_unity, (h_unity * 2.5f) + i, w_unity * 7, (h_unity * 2.5f) + i, paint);
                for (int i = 0; i < 3; i++) canvas.DrawLine(w_unity, (h_unity * 10.5f) + i, w_unity * 7, (h_unity * 10.5f) + i, paint);
                //Graphic Tackes the 

            }

        }

    }
    //Wraps a String
    class labelrender
    {
        public String Text;
        public labelrender(){}
    }
    //Wraps the x y x values from the accelerometer
    class accelerometerValue
    {
        public double x, y, z;
        public accelerometerValue(double _x , double _y, double _z)
        {
            x = _x; y = _y; z = _z;
        }

    }
}
