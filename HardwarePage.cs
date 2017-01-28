using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Safe
{
    class HardwarePage : CustomPage
    {
        Accelerometer acceler;
        Gps gps;

        SKCanvasView view;

        DataReader data_reader;

        List<VectorValue> accelerometer_data;
        List<VectorValue> gps_data;

        static readonly int DATA_BUFFER_SIZE = 50;

        static readonly int UPDATE_DELAY = 50; //In ms
        static int paint_counter = 0;

        


        public HardwarePage()
        {

            initColors();

            //List for store the accelerometer data
            accelerometer_data = new List<VectorValue>();
            gps_data = new List<VectorValue>();
            
            //Sensors
            acceler = new Accelerometer(accelerometer_data, DATA_BUFFER_SIZE);
            gps = new Gps(gps_data, DATA_BUFFER_SIZE,UPDATE_DELAY);

            //SKiasharp view
            view = new SKCanvasView();
            view.PaintSurface += View_PaintSurface;
            Content = view;
    
            //datareader
            data_reader = new DataReader(accelerometer_data, gps_data);
            data_reader.startReadings();

            //synchronyzed task
            Task.Factory.StartNew(
                () => update(),
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
                gps.getGpsLocation();
                await Task.Delay(UPDATE_DELAY); //Delay between updates
            }
        }

        //Event wich is called when the surface should be repainted
        private void View_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            int uheigth, uwidth, H_DIV, W_DIV;
            int middle_width, radius;
            SKCanvas canvas = e.Surface.Canvas;
            int surfaceWidth = e.Info.Width;
            int surfaceHeight = e.Info.Height;
            H_DIV = 16; W_DIV = 8; //height and width divisions

            uwidth = surfaceWidth / W_DIV;
            uheigth = surfaceHeight / H_DIV;
            radius = uwidth * 3;
            middle_width = surfaceWidth / 2;


            using (SKPaint paint = new SKPaint())
            {
                canvas.Clear();
                paint.IsAntialias = true;

                //Gradient
                createGradient(canvas, paint,surfaceWidth,surfaceHeight);
               
                paint_counter++;
                VectorValue glast, alast; //Will contain the las element in the lists

                //Titles
                paint.Shader = SKShader.CreateColor(TEXT);
                paint.TextSize = uheigth;
                canvas.DrawText("GPS:", uwidth, uheigth * 2, paint);
                canvas.DrawText("Accelerometer:", uwidth, uheigth * 7, paint);

                //Data
                paint.TextSize = uheigth/2;
                if (gps_data.Count >= 1)
                {
                    glast = gps_data[gps_data.Count - 1];
                    canvas.DrawText(string.Format("Lat:[{0:N4}],Lon:[{1:N4}]", glast.x, glast.y), 2 * uwidth, 4 * uheigth, paint); //Paint the values of the last element in the buffer
                    canvas.DrawText(string.Format("Speed:[{0:N4}](Km/h)",gps.calculateSpeed()), 2 * uwidth, 5 * uheigth, paint);
                }
                else
                {   //If the buffer it's still empty
                    canvas.DrawText("Loading GPS...", 2 * uwidth, 4 * uheigth, paint);
                    glast = null;
                }
                if (accelerometer_data.Count >= 1)
                {
                    alast = accelerometer_data[accelerometer_data.Count - 1];
                    canvas.DrawText(string.Format("x[{0:N3}]y[{1:N3}]z[{2:N3}]", alast.x, alast.y, alast.z), 2 * uwidth, 9 * uheigth, paint);
                }
                else
                {   //If the buffer it's still empty
                    canvas.DrawText("Loading accelerometer...", 2 * uwidth, 9 * uheigth, paint);
                    alast = null;
                }

                //Lines
                for (int i = 0; i < 3; i++) canvas.DrawLine(uwidth, (uheigth * 2.5f) + i, uwidth * 7, (uheigth * 2.5f) + i, paint);
                for (int i = 0; i < 3; i++) canvas.DrawLine(uwidth, (uheigth * 7.5f) + i, uwidth * 7, (uheigth * 7.5f) + i, paint);

                //Graphic
                int left_margin, right_margin, middle_garph;
                left_margin = uwidth; right_margin = uwidth * 7; middle_garph = uheigth * 12;

                canvas.DrawLine(2 * uwidth, middle_garph, uwidth * 6, middle_garph, paint);  //Middle grafico
                canvas.DrawLine(3 * uwidth, uheigth * 10, uwidth * 5, uheigth * 10, paint);  //Top
                canvas.DrawLine(3 * uwidth, uheigth * 14, uwidth * 5, uheigth * 14, paint);  //Bottom

               

                if (alast != null)
                {
                    float x, y, z;
                    x = (float)alast.x / 5;
                    y = (float)alast.y / 5;
                    z = (float)alast.z / 5;
                    int graphbar_width = (int)((4.0 / 3.0) * uwidth);
                    int x_init = 2 * uwidth;

                    //The first bar in the graph
                    paint.Shader = GRAPH0;
                    if (alast.x > 0)
                    {
                        canvas.DrawRect(new SKRect(x_init, middle_garph - (x * uheigth), 
                                                  (x_init + graphbar_width), middle_garph), paint); //X red
                    }
                    else
                    {   //In this case x will have a negative value, h+uheigth too, so we have to use abs()
                        canvas.DrawRect(new SKRect(x_init, middle_garph, 
                                                  (x_init + graphbar_width) ,middle_garph + (Math.Abs(x) * uheigth)), paint); //X red
                    }
                    //The second bar in the graph
                    paint.Shader = GRAPH1;
                    if (alast.y > 0)
                    {
                        canvas.DrawRect(new SKRect((x_init + graphbar_width), middle_garph - (y * uheigth), 
                                                   (x_init + 2 * graphbar_width), middle_garph), paint); 
                    }
                    else
                    {
                        canvas.DrawRect(new SKRect((x_init + graphbar_width), middle_garph, 
                                                   (x_init +  2 * graphbar_width), middle_garph + (Math.Abs(y) * uheigth)), paint);
                    }
                    //The thirth bar in the graph
                    paint.Shader = GRAPH2;
                    if (alast.z > 0)
                    {
                        canvas.DrawRect(new SKRect((x_init + 2 * graphbar_width), middle_garph - (z * uheigth), 
                                                   (x_init + 3 * graphbar_width), middle_garph), paint); 
                    }
                    else
                    {
                        canvas.DrawRect(new SKRect((x_init + 2 * graphbar_width), middle_garph, 
                                                   (x_init + 3 * graphbar_width), middle_garph + (Math.Abs(z) * uheigth)), paint); 
                    }   
                }
               // displayLayout(canvas, paint, uheigth, uwidth);
            }
        }
        private void displayLayout(SKCanvas canvas, SKPaint paint, int uheigth, int uwidth)
        {
            for (int i = 0; i < 8; i++)
            {
                canvas.DrawLine(uwidth * i, 0, uwidth * i, uheigth * 16, paint); //Vertical
            }
            for (int i = 0; i < 16; i++)
            {
                canvas.DrawLine(0, uheigth * i, uwidth * 8, uheigth * i, paint); //horizontal
            }
        }
    }
    //Wraps the x y z values from the accelerometer
    class VectorValue
    {
        public double x, y, z;
        public DateTimeOffset stamp;
        public VectorValue(double _x, double _y, double _z)
        {
            x = _x; y = _y; z = _z;
        }

        public VectorValue(double _x, double _y, DateTimeOffset _z)
        {
            x = _x; y = _y; stamp = _z;
        }
        public override string ToString()
        {
            return string.Format("[{0:N3},{1:N3},{2:N3}]", x, y, z);
        }
    }
}
