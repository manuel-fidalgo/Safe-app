using Plugin.Geolocator;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;

namespace Safe
{
    class Gps 
    {
        labelrender gps_label;
        readonly int TASK_DELAY = 500; //Time for each gps update
        public static readonly int ACCURACY = 1; //Acuracy for the gps (meters)
        static bool ON = true;
        static Gps gps_singleton;

        IGeolocator locator;

        public Gps(labelrender label)
        {
            gps_label = label;
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = ACCURACY;
            gps_singleton = this;
        }

        public static Gps getGps()
        {
            return gps_singleton;
        }
        
        //Changes the render label each time this method is called
        public async void getGpsLocation()
        {
            
            try
            {
                var position = await locator.GetPositionAsync(10000);
                gps_label.Text = string.Format("Lat[{0}] Lon[{1}]", position.Latitude, position.Longitude);
            }
            catch(Exception e) {
                gps_label.Text = string.Format("Fail");
            }
        }
    }
}