using Plugin.Geolocator;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;




namespace Safe
{
    public class Gps 
    {
        public Label gps_label;
        Task task;

        public Gps(Label label)
        {
            gps_label = label;
            getGpsLocation();
            initControlTack();
        }

        private void initControlTack()
        {
            task = Task.Run(() => {
                while (true)
                {
                    getGpsLocation(); //Obtiene la nueva ubicacion del gps y la muestra por pantalla
                }
            });
        }

        public async void getGpsLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            try
            {
                var position = await locator.GetPositionAsync(10000);
                System.Diagnostics.Debug.WriteLine("Position Status: {0}", position.Timestamp);
                System.Diagnostics.Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                System.Diagnostics.Debug.WriteLine("Position Longitude: {0}", position.Longitude);
                gps_label.Text = string.Format("Latitute[{0}]\nLongitude[{1}]", position.Latitude, position.Longitude);
            }
            catch(Exception e) {
                System.Diagnostics.Debug.WriteLine("Localitacion exception"+e.Data);
            }
        }
    }
}