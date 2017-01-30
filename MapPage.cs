using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace Safe
{
    public class MapPage : ContentPage
    {
        public static VectorValue last_coordinates;

        public MapPage()
        {
            
            var map = new Map(MapSpan.FromCenterAndRadius(new Position(last_coordinates.x,last_coordinates.y), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
            
        }
    }
}
