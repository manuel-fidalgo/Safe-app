using Plugin.Geolocator;
using System;
using System.Linq;
using Xamarin.Forms;



namespace Safe
{
    public class Gps 
    {
        public Label gps_label;

        public Gps(Label label)
        {
            gps_label = label;
            initGps();
        }

        public async void initGps()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(10000);

            System.Diagnostics.Debug.WriteLine("Position Status: {0}", position.Timestamp);
            System.Diagnostics.Debug.WriteLine("Position Latitude: {0}", position.Latitude);
            System.Diagnostics.Debug.WriteLine("Position Longitude: {0}",position.Longitude);
            gps_label.Text = string.Format("Latitute[{0}]\nLongitude[{1}]",position.Latitude, position.Longitude);

        }
    }
}