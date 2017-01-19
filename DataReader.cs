using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

/**
 * This class wraps the algorithm for controll if there is a fall or an accident
 */
namespace Safe
{
    class DataReader
    {
        List<VectorValue> accelerometer_data;
        List<VectorValue> gps_data;
        ContentPage notification_page;

        public DataReader(List<VectorValue> a_data, List<VectorValue> g_data)
        {
            notification_page = new ContentPage();
            accelerometer_data = a_data;
            gps_data = g_data;
        }

        public void startReadings()
        {
            //Creates the background task
            Task.Factory.StartNew(
                () => controlData(),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext()
                );
        }

        private async Task controlData()
        {
            while (true)
            {
                await Task.Delay(3000); //Each three secinds
                exploreAccelerometerValues();
            }
        }

        private void exploreAccelerometerValues()
        {
            lock (accelerometer_data)
            {
                int cero_values=0;
                foreach (var value in accelerometer_data)
                {
                    if((value.x<0.5||value.x>0.5) && (value.y<0.5||value.y>0.5) && (value.z < 0.5 || value.z > 0.5))
                    {
                        cero_values++;
                    }
                }
                if (cero_values >= 2)
                {
                    //pushNotification();
                }
            }
        }

        private async void pushNotification()
        {
            var answer = await notification_page.DisplayAlert("ALERT", "Are you okay?", "Yes", "No");
            if (answer)
            {
                //Is okay
            }else
            {
                sendAlertMessage();
            }
        }

        private void sendAlertMessage()
        {
            //YOKSETIO
        }
    }
    
}
