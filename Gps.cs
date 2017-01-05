using System;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms;

namespace Safe
{
    public class Gps : ILocationListener
    {
        Label gps_label;
        public Gps(Label lbl)
        {
            gps_label = lbl;
        }

        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OnLocationChanged(Location location)
        {
            gps_label.Text = string.Format("lat: [%f], len[%f]",location.Latitude,location.Latitude);
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}