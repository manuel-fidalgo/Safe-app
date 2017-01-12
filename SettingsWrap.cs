using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Safe
{
    //This class will writte and read from the settings file

    class SettingsWrap
    {
        private static readonly string SETTINGS_PATH = "settings.xml";
        public static readonly string DEFAULT_PASS = "****";

        private static Boolean gps_status;
        private static Boolean accelerometer_status;
        private static Boolean vibration_status;

        private static Boolean security_code_status;
        private static String seccurity_code;

        private static String contact_Message;
        private static String contact_number;
        private static String emergency_number;

        private static String currentLanguage;
        private static String currentLanguage_code;

        private static XDocument settings_file;

        //XML conexion methods
        public static void loadSettingsfromXML()
        {
            
            settings_file = XDocument.Load(SETTINGS_PATH);
            IEnumerable<XElement> settings_list = settings_file.Elements();
            foreach ( var setting in settings_list)
            {
                Debug.WriteLine(setting);
            }
            

        }

        public static void writeSettingsintoXML()
        {

        }


        //GPS
        public static void set_gps_status(Boolean b)
        {
            gps_status = b;
        }

        public static Boolean get_gps_status()
        {
            return gps_status;
        }

        //Accelerometer
        public static void set_accelerometer_status(Boolean b)
        {
            accelerometer_status = b;
        }
        public static Boolean get_accelerometer_status()
        {
            return accelerometer_status;
        }

        //Vibration
        public static void set_vibration_status(Boolean b)
        {
            vibration_status = b;
        }
        public static Boolean get_vibration_status()
        {
            return vibration_status;
        }

        //Security code
        public static void set_seccode_status(Boolean b)
        {
            security_code_status = b;
        }
        public static Boolean get_seccode_status()
        {
            return security_code_status;
        }
        public static void set_securityCode(String code)
        {
            seccurity_code = code;
        }
        public static string get_securityCode()
        {
            return seccurity_code;
        }

        //Personal info
        public static void setContactMessage(String message)
        {
            contact_Message = message;
        }
        public static string getContactMessage()
        {
            return contact_Message;
        }
        public static void setContactNumber(String number)
        {
            contact_number = number;
        }
        public static string getContactNumber()
        {
            return contact_number;
        }
        public static void setEmergencyNumber(String number)
        {
            emergency_number = number;
        }
        public static string getEmergencyNumber()
        {
            return emergency_number;
        }

        //Language
        public static void changeLenguage(String language)
        {
            currentLanguage = language;
        }
        public static string getCurrentLenguaje()
        {
            return currentLanguage;
        }
    }
}
