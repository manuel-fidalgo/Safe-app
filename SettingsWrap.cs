using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe
{
    //This class will writte and read from the settings file

    class SettingsWrap
    {
        
        //GPS
        public static void change_gps_status()
        {

        }
        public static Boolean get_gps_status()
        {
            return false;
        }

        //Accelerometer
        public static void change_accelerometer_status()
        {

        }
        public static Boolean get_accelerometer_status()
        {
            return false;
        }

        //Vibration
        public static void change_vibration_status()
        {

        }
        public static Boolean get_vibration_status()
        {
            return false;
        }

        //Security code
        public static void change_seccode_status()
        {

        }
        public static Boolean get_seccode_status()
        {
            return false;
        }
        public static void set_securityCode(String code)
        {

        }
        public static string get_securityCode()
        {
            return "";
        }

        //Personal info
        public static void setContactMessage(String message)
        {

        }
        public static string getContactMessage()
        {
            return "";
        }
        public static void setContactNumber(String number)
        {

        }
        public static string getContactNumber()
        {
            return "";
        }
        public static void setEmergencyNumber(String number)
        {

        }
        public static string getEmergencyNumber()
        {
            return "";
        }

        //Language
        public static void changeLenguaje(String language)
        {

        }
        public static string getCurrentLenguaje()
        {
            return "";
        }
    }
}
