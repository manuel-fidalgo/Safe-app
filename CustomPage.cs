using System;
using Xamarin.Forms;
using SkiaSharp;
using System.IO;
using System.Reflection;

namespace Safe
{
    public abstract class CustomPage : ContentPage
    {
        protected SKColor BALL, TEXT, GRADIENT0, GRADIENT1;
        protected SKShader GRAPH0, GRAPH1, GRAPH2, BLUEGRADIENT;


        
        protected void initColors()
        {
           
            if (SettingsWrap.use_accesibility) TEXT = new SKColor(255,255,255);
            else TEXT = new SKColor(165, 167, 159);
            BALL = new SKColor(0, 100, 255);
            GRADIENT0 = new SKColor(110, 110, 110);
            GRADIENT1 = new SKColor(37, 40, 42);

            //GRAPH0 = SKShader.CreateColor(new SKColor(80, 240, 60));
            GRAPH0 = SKShader.CreateColor(new SKColor(22, 122, 32));
            GRAPH1 = SKShader.CreateColor(new SKColor(255, 255, 255));
            GRAPH2 = SKShader.CreateColor(new SKColor(250, 0, 0));

            var colors = new SKColor[] { new SKColor(0,100,255), new SKColor(0,0,255) };

            BLUEGRADIENT = SKShader.CreateLinearGradient(new SKPoint(0, 0), new SKPoint(50, 50),
                                                        colors, null, SKShaderTileMode.Clamp);

        }

        //Creates and paint the background gradient
        protected void createGradient(SKCanvas canvas, SKPaint paint,int surfaceWidth, int surfaceHeight)
        {
            var colors = new SKColor[] { GRADIENT0, GRADIENT1 };

            var shader = SKShader.CreateRadialGradient(new SKPoint(surfaceWidth / 2, surfaceHeight / 2),
                                                        surfaceWidth / 2, colors, null, SKShaderTileMode.Clamp);
            paint.Shader = shader;
            canvas.DrawPaint(paint);
            
        }
        protected void drawImage(SKCanvas canvas, SKRect place ,String path)
        {
            var assembly = typeof(CustomPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(path);

            // decode the bitmap from the stream
            using (var stream_ = new SKManagedStream(stream))
            using (var bitmap = SKBitmap.Decode(stream_))
            using (var paint = new SKPaint())
            {
                canvas.DrawBitmap(bitmap, place, paint);
            }
        }
    }
}
