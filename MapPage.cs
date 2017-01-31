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
        public static Position default_position = new Position(37, -122);

        public MapPage()
        {
            Position pos;
            if (last_coordinates != null)
            {
                pos = new Position(last_coordinates.x,last_coordinates.y);
            }
            else
            {
                pos = default_position;
            }

            var map = new Map(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            map.MapType = MapType.Street;
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
        }
    }
}

