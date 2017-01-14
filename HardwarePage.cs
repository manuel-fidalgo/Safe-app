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

        SKCanvasView view;

        List<VectorValue> accelerometer_data;
        List<VectorValue> gps_data;

        static readonly int DATA_BUFFER_SIZE = 300;

        static readonly int UPDATE_DELAY = 100;
        static int paint_counter = 0;

        enum PORTION
        {
            ONE = 1, TWO = 2, THREE = 3, FOUR = 4, FIVE = 5, SIX = 6, SEVEN = 7, EIGHT = 8
        }

        public HardwarePage()
        {

            //List for store the accelerometer data
            accelerometer_data = new List<VectorValue>();
            gps_data = new List<VectorValue>();

            //Sensors
            acceler = new Accelerometer(accelerometer_data, DATA_BUFFER_SIZE);
            gps = new Gps(gps_data, DATA_BUFFER_SIZE);

            //SKiasharp view
            view = new SKCanvasView();
            view.PaintSurface += View_PaintSurface;
            Content = view;

            //synchronyzed task
            Task.Factory.StartNew(
                () => this.update(),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext()
                );
        }

        //Update Loop
        private async Task update()
        {
            while (true)
            {
                view.InvalidateSurface(); //Repaint method
                Gps.getGps().getGpsLocation();
                await Task.Delay(UPDATE_DELAY); //Delay between updates
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
                paint_counter++;
                VectorValue glast, alast; //Will contain the las element in the lists

                //Background
                paint.Color = new SKColor(0xe5, 0xef, 0xff);
                canvas.DrawRect(new SKRect(0, 0, surfaceWidth, surfaceHeight), paint);

                //Titles
                paint.Color = new SKColor(0x00, 0x00, 0x00);
                canvas.DrawText("Paint no->" + paint_counter, 10, 10, paint);
                paint.TextSize = 70;
                canvas.DrawText("GPS:", w_unity, h_unity * 2, paint);
                canvas.DrawText("Accelerometer:", w_unity, h_unity * 7, paint);

                //Data
                paint.TextSize = 35;
                if (gps_data.Count >= 1)
                {
                    glast = gps_data[gps_data.Count - 1];
                    canvas.DrawText(string.Format("Lat[{0:N4}],lon[{1:N4}]", glast.x, glast.y), 2 * w_unity, 4 * h_unity, paint); //Paint the values of the last element in the buffer
                }
                else
                {   //If the buffer it's still empty
                    canvas.DrawText("Loading GPS...", 2 * w_unity, 4 * h_unity, paint);
                    glast = null;
                }
                if (accelerometer_data.Count >= 1)
                {
                    alast = accelerometer_data[accelerometer_data.Count - 1];
                    canvas.DrawText(string.Format("x[{0:N3}]y[{1:N3}]z[{2:N3}]", alast.x, alast.y, alast.z), 2 * w_unity, 9 * h_unity, paint);
                }
                else
                {   //If the buffer it's still empty
                    canvas.DrawText("Loading accelerometer...", 2 * w_unity, 9 * h_unity, paint);
                    alast = null;
                }

                //Lines
                paint.Color = new SKColor(0x28, 0x2c, 0xff);  //0x282cff
                for (int i = 0; i < 3; i++) canvas.DrawLine(w_unity, (h_unity * 2.5f) + i, w_unity * 7, (h_unity * 2.5f) + i, paint);
                for (int i = 0; i < 3; i++) canvas.DrawLine(w_unity, (h_unity * 7.5f) + i, w_unity * 7, (h_unity * 7.5f) + i, paint);

                //Graphic
                int left_margin, right_margin, middle_garph;
                left_margin = w_unity; right_margin = w_unity * 7; middle_garph = h_unity * 12;

                paint.Color = new SKColor(0x00, 0x00, 0x00);
                canvas.DrawLine(2 * w_unity, middle_garph, w_unity * 6, middle_garph, paint);  //Middle grafico
                canvas.DrawLine(3 * w_unity, h_unity * 10, w_unity * 5, h_unity * 10, paint);  //Top
                canvas.DrawLine(3 * w_unity, h_unity * 14, w_unity * 5, h_unity * 14, paint);  //Bottom

                SKColor red, green, blue;
                red = new SKColor(0xff, 0x00, 0x00);
                green = new SKColor(0x00, 0xff, 0x00);
                blue = new SKColor(0x00, 0x00, 0xff);

                if (alast != null)
                {
                    float x, y, z;
                    x = (float)alast.x / 5;
                    y = (float)alast.y / 5;
                    z = (float)alast.z / 5;

                    paint.Color = red;
                    if (alast.x > 0)
                    {
                        canvas.DrawRect(new SKRect(2 * w_unity, middle_garph - (x * h_unity), 3 * w_unity, middle_garph), paint); //X red
                    }
                    else
                    {   //In this case x will have a negative value, h+h_unity also, so if we want to add the variation we have to use abs()
                        canvas.DrawRect(new SKRect(2 * w_unity, middle_garph, 3 * w_unity, middle_garph + (Math.Abs(x) * h_unity)), paint); //X red
                    }

                    paint.Color = green;
                    if (alast.y > 0)
                    {
                        canvas.DrawRect(new SKRect(3 * w_unity, middle_garph - (y * h_unity), 4 * w_unity, middle_garph), paint); //Y green
                    }
                    else
                    {
                        canvas.DrawRect(new SKRect(3 * w_unity, middle_garph, 4 * w_unity, middle_garph + (Math.Abs(y) * h_unity)), paint); //Y green
                    }

                    paint.Color = blue;
                    if (alast.z > 0)
                    {
                        canvas.DrawRect(new SKRect(4 * w_unity, middle_garph - (z * h_unity), 5 * w_unity, middle_garph), paint); //Z Blue
                    }
                    else
                    {
                        canvas.DrawRect(new SKRect(4 * w_unity, middle_garph, 5 * w_unity, middle_garph + (Math.Abs(z) * h_unity)), paint); //Z Blue
                    }
                }

            }

        }

    }
    //Wraps the x y z values from the accelerometer
    class VectorValue
    {
        public double x, y, z;
        public VectorValue(double _x, double _y, double _z)
        {
            x = _x; y = _y; z = _z;
        }
        public override string ToString()
        {
            return string.Format("[{0:N3},{1:N3},{2:N3}]", x, y, z);
        }
    }
}
