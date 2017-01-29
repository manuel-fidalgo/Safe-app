using Plugin.Vibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Safe
{
    class VibrationManager
    {
        public static void vibrate(int ms)
        {
            var v = CrossVibrate.Current;
            v.Vibration(ms); // 1 second vibration
        }
    }
    internal  class MessageManager
    {
        public static void sendAlertMessage()
        {
            System.Diagnostics.Debug.WriteLine("Message sucess");
            //TODO
        }
    }
}
