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
            bool c = false;
            lock (accelerometer_data)
            {
                foreach (var value in accelerometer_data)
                {
                    if (System.Math.Abs(value.x)>13 || System.Math.Abs(value.y)>13 || System.Math.Abs(value.z)>13)
                    {
                        c = true;
                        accelerometer_data.Remove(value);
                    }
                    if (c) pushNotification();
                }
            }
        }

        private async void pushNotification()
        {
            var answer = await notification_page.DisplayAlert("ALERT", "Are you okay?", "Yes", "No");
            if (!answer)
            {
                sendAlertMessage();   
            }
        }

        private void sendAlertMessage()
        {

        }
    }
    
}
