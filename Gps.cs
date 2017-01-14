using Plugin.Geolocator;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;
using System.Collections.Generic;

namespace Safe
{
    class Gps 
    {
        
        public static readonly int ACCURACY = 1; //Acuracy for the gps (meters)
        static Gps gps_singleton;

        List<VectorValue> gps_data;
        int buffer_size;
        int buffer_counter;

        IGeolocator locator;

        

        public Gps(List<VectorValue> buff, int buff_size)
        {
            
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = ACCURACY;
            gps_singleton = this;

            gps_data = buff;
            buffer_size = buff_size;
            buffer_counter = 0;
        }

        //Singleton pattern for create
        public static Gps getGps()
        {
            return gps_singleton;
        }
        
        //Add the last location to the buffer
        public async void getGpsLocation()
        {
            try
            {
                var position = await locator.GetPositionAsync(10000);

                if(buffer_counter == buffer_size)
                {
                    gps_data.RemoveAt(0);
                    buffer_counter--;
                }
                gps_data.Add(new VectorValue(position.Latitude,position.Longitude,position.Altitude));
                buffer_counter++;
                
            }
            catch (Exception){
                gps_data.Add(new VectorValue(0,0,0));
            }
        }
    }
}