using Plugin.Geolocator;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;

namespace Safe
{
    public class Gps 
    {
        public Label gps_label;
        Task task;
        readonly int TASK_DELAY = 500; //Time for each gps update
        public static readonly int ACCURACY = 1; //Acuracy for the gps (meters)
        static bool ON = true;

        IGeolocator locator;


        public Gps(Label label)
        {
            gps_label = label;
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = ACCURACY;
            if(ON) startGps();
        }
        /*
         * 
         */
        private void startGps()
        {
            Task.Factory.StartNew(
                () => this.updateLocation(),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        public async Task updateLocation()
        {
            while (true)
            {
                getGpsLocation(); //Gets the new location and modify the labels in the screen
                await Task.Delay(TASK_DELAY);
            }
        }

        public async void getGpsLocation()
        {
            try
            {
                var position = await locator.GetPositionAsync(10000);
                gps_label.Text = string.Format("Latitute[{0}]\nLongitude[{1}]", position.Latitude, position.Longitude);
               // System.Diagnostics.Debug.WriteLine("Position Status: {0}", position.Timestamp); System.Diagnostics.Debug.WriteLine("Position Latitude: {0}", position.Latitude); System.Diagnostics.Debug.WriteLine("Position Longitude: {0}", position.Longitude);
            }
            catch(Exception e) {
                System.Diagnostics.Debug.WriteLine("Localitacion exception"+e.Data);
            }
        }
    }
}