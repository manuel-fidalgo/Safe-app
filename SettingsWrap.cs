using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Safe
{
    //This class will writte and read from the settings file

    static class SettingsWrap
    {

        public static readonly string SETTINGS_PATH = "Safe.SettingsFile.txt";
        public static readonly string DEFAULT_PASS = "****";

        public static Boolean crash_status;
        public static Boolean falls_status;
        public static Boolean vibration_status;

        public static Boolean use_accesibility;

        public static Boolean security_code_status;
        public static String seccurity_code;

        public static Boolean use_personal_message;
        public static String contact_Message;
        public static String contact_number;

        public static String currentLanguage_code;


        public static void loadSettingsfromFile()
        {
            var assembly = typeof(SettingsWrap).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(SETTINGS_PATH);
            using (var reader = new System.IO.StreamReader(stream))
            {

                crash_status = Boolean.Parse(remove_comment(reader.ReadLine())); 
                falls_status = Boolean.Parse(remove_comment(reader.ReadLine()));
                vibration_status = Boolean.Parse(remove_comment(reader.ReadLine()));

                security_code_status = Boolean.Parse(remove_comment(reader.ReadLine()));
                seccurity_code = remove_comment(reader.ReadLine());

                use_personal_message = Boolean.Parse(remove_comment(reader.ReadLine()));
                contact_Message = remove_comment(reader.ReadLine());
                contact_number = remove_comment(reader.ReadLine());
                
                currentLanguage_code = remove_comment(reader.ReadLine());
                use_accesibility = Boolean.Parse(remove_comment(reader.ReadLine()));
                
            }
        }
        private static String remove_comment(String s)
        {
            return s.Substring(0, s.IndexOf('#')).Trim();
        } 
        //COONTROLAR QUE NO QUEDEN COSAS VACIAS QUE PUEDAN ALTERAR LA LECTURA O ESCRITURA DEL FICHERO
        //ERROR STREAM WAS NO WRITTABLE
        public static void writeSettingsintoFile()
        {
            try
            {
                var assembly = typeof(SettingsWrap).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream(SETTINGS_PATH);
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(crash_status.ToString());
                    writer.WriteLine(falls_status.ToString());
                    writer.WriteLine(vibration_status.ToString());

                    writer.WriteLine(security_code_status.ToString());
                    if (!security_code_status)
                        seccurity_code = DEFAULT_PASS;

                    writer.WriteLine(seccurity_code);
                    writer.WriteLine(contact_Message);

                    writer.WriteLine(currentLanguage_code);

                }
            }
            catch (Exception)
            {
                //Some bad ocurred
            }
        }
    }
}