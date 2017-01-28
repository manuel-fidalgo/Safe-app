using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Reflection;
using System.Threading;

namespace Safe
{ 
    public abstract class CustomPage : ContentPage
    {
        protected SKColor BALL, TEXT, GRADIENT0, GRADIENT1;
        protected SKShader GRAPH0, GRAPH1, GRAPH2;

        protected void initColors()
        {
            BALL = new SKColor(255, 0, 0);
            TEXT = new SKColor(165, 167, 159);
            GRADIENT0 = new SKColor(110, 110, 110);
            GRADIENT1 = new SKColor(37, 40, 42);

            GRAPH0 = SKShader.CreateColor(new SKColor(0, 255, 0));
            GRAPH0 = SKShader.CreateColor(new SKColor(255, 255, 255));
            GRAPH0 = SKShader.CreateColor(new SKColor(255, 0, 0));
        }

        //Creates and paint the background gradient
        protected void createGradient(SKPaint paint,int surfaceWidth, int surfaceHeight)
        {
            var colors = new SKColor[] { GRADIENT0, GRADIENT1 };

            var shader = SKShader.CreateRadialGradient(new SKPoint(surfaceWidth / 2, surfaceWidth / 2),
                                                        surfaceWidth / 3, colors, null, SKShaderTileMode.Clamp);

            var shader_ = SKShader.CreateLinearGradient(new SKPoint(0,0), new SKPoint(surfaceWidth,surfaceHeight),
                                                        colors,null,SKShaderTileMode.Clamp);
            paint.Shader = shader_;
        }
    }
}
