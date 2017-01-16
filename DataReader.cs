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

        public DataReader(List<VectorValue> a_data, List<VectorValue> g_data)
        {
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
                await Task.Delay(1000);
            }
        }
        private void pushNotification()
        {
            
           
            
        }

    }
    
}
