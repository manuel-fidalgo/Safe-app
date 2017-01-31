using Plugin.Geolocator;
using System;
using Plugin.Geolocator.Abstractions;
using System.Collections.Generic;

namespace Safe
{
    class Gps 
    {
        
        public static readonly int ACCURACY = 1; //Acuracy for the gps (meters)

        List<VectorValue> gps_data;
        List<double> gps_speed_buffer;

        int buffer_size;
        int buffer_counter;
        int buffer_speed_counter;

        IGeolocator locator;

        int paintdelay;
        double lastSpeed = 0;

        double _eQuatorialEarthRadius = 6378.1370D;
        double _d2r = (Math.PI / 180D);

        public Gps(List<VectorValue> buff, int buff_size,int delay)
        {

            paintdelay = delay;
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = ACCURACY;
            gps_speed_buffer = new List<double>();

            gps_data = buff;
            buffer_size = buff_size;
            buffer_counter = 0; buffer_speed_counter = 0;
        }

        VectorValue last_added;
        //Add the last location to the buffer
        public async void getGpsLocation()
        {
            VectorValue coor;
         
            try
            {
                var position = await locator.GetPositionAsync(10000);
                
                if(buffer_counter == buffer_size)
                {
                    gps_data.RemoveAt(0);
                    buffer_counter--;
                }
                coor = new VectorValue(position.Latitude, position.Longitude, position.Timestamp);
                gps_data.Add(coor);
                MapPage.last_coordinates = coor; //Will update the las coordinates in the map page 
                buffer_counter++;
                last_added = coor; //The location is correct
            }
            catch (Exception){
                //Any error, we are goint to repeat the last correct value
                gps_data.Add(last_added);
            }
        }

        //Gets the distance betweem two coordinates (Meters)
        private int HaversineInM(double lat1, double long1, double lat2, double long2)
        {
            return (int)(1000D * HaversineInKM(lat1, long1, lat2, long2));
        }

        //Gets the distance betweem two coordinates (Meters)
        private double HaversineInKM(double lat1, double long1, double lat2, double long2)
        {
            double dlong = (long2 - long1) * _d2r;
            double dlat = (lat2 - lat1) * _d2r;
            double a = Math.Pow(Math.Sin(dlat / 2D), 2D) + Math.Cos(lat1 * _d2r) * Math.Cos(lat2 * _d2r) * Math.Pow(Math.Sin(dlong / 2D), 2D);
            double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
            double d = _eQuatorialEarthRadius * c;

            return d;
        }

        //Calulate the speed using the last two coordinates and the delay between two refresh
        public double calculateSpeed()
        {
            
            if (buffer_counter > 2)
            {
                double speed_ret = 0;

                VectorValue p1 = gps_data[gps_data.Count - 2];
                VectorValue p2 = gps_data[gps_data.Count - 1];

                if (p1.x == p2.x && p1.y == p2.y) return 0; //Both are the same
                if (p1.x == 0 && p1.y == 0 || p2.x == 0 && p2.y == 0) return lastSpeed; //Any corrdinality is wrong

                var dist = HaversineInM(p1.x,p1.y,p2.x,p2.y);
                DateTime t1, t2;
                t1 = p1.stamp.DateTime;
                t2 = p2.stamp.DateTime;

                var time_s = (int)((t2-t1).TotalMilliseconds) / 1000.0; 
                double speed_mps = dist / time_s;
                lastSpeed = speed_mps;

                if (buffer_speed_counter == buffer_size)
                {
                    gps_speed_buffer.RemoveAt(0);
                    buffer_speed_counter--;
                }
                speed_ret = 3.6 * speed_mps;
                gps_speed_buffer.Add(speed_ret);
                buffer_speed_counter++;
                return speed_ret;

            }
            else
            {   //No such data for calculate the speed
                return -1;
            }
        }
    }
}