using Xamarin.Forms;

namespace Safe
{
    class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
           
            var gps_sw_cell = new SwitchCell
            {
                Text = "GPS detection"
            };
            var accelerometer_cell = new SwitchCell
            {
                Text = "Accelerometer detection"
            };

            var gpsaccelerometer_section = new TableSection("GPS and accelerometer")
            {
                gps_sw_cell,
                accelerometer_cell
            };
            
            //Password configuration

            var password_cell = new SwitchCell
            {
                Text = "Password"
            };
            var password_text = new EntryCell
            {
                Text = "Password"
            };
            var password_setcion = new TableSection("Password config")
            {
                password_cell,
                password_text
            };

            //Personal data configuration

            var contact_message = new EntryCell
            {
                Text = "Conctac Message"
            };
            var contact_number = new EntryCell
            {
                Text = "Contact Number"
            };
            var emergency_number = new EntryCell
            {
                Text = "Emergency number"
            };
            var personal_section = new TableSection("Personal info")
            {
                contact_message,
                contact_number,
                emergency_number
            };

            Content = new TableView
            {
                Root = new TableRoot
                {
                    gpsaccelerometer_section,
                    password_setcion,
                    personal_section,
                }, 
            };
        }
    }
}
