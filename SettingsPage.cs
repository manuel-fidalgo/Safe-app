using System;
using Xamarin.Forms;

namespace Safe
{
    class SettingsPage : ContentPage
    {

        LanguagesList lg_lst;

        public SettingsPage()
        {

            lg_lst = new LanguagesList();

            //Hardware config
            
            var gps_cell = new SwitchCell
            {
                Text = "GPS detection"
            };

            var accelerometer_cell = new SwitchCell
            {
                Text = "Accelerometer detection"
            };

            var vibration_cell = new SwitchCell
            {
                Text = "Vibration"
            };

            var hardware_section = new TableSection("Hardware Config")
            {
                gps_cell,
                accelerometer_cell,
                vibration_cell
            };
            
            //Password configuration

            var password_cell = new SwitchCell
            {
                Text = "Use a Security code",
                On = false
            };

            var password_text = new EntryCell
            {
                Label = "Security code",
                Text = "****"
            };

            var password_setcion = new TableSection("Password config")
            {
                password_cell,
                password_text
            };

            //Personal data configuration

            var contact_message = new EntryCell
            {
                Label = "Conctact Message:  "
            };

            var contact_number = new EntryCell
            {   
                Label = "Contact number:  ",
            };

            var emergency_number = new EntryCell
            {
                Label = "Emergency number  ",
                LabelColor = Color.FromRgb(255,100,100),
            };

            var personal_section = new TableSection("Personal info")
            {
                contact_message,
                contact_number,
                emergency_number
            };

            //Language cell

            var languagecell = new LanguageCell(this);

            var language_section = new TableSection("Language")
            {
                languagecell
            };
            
            //Save button

            var save_button = new Button
            {
                Text = "Save",
                TextColor = Color.Aqua
            };
            
            //Setings tableview

             var settings_table = new TableView
             {
                 Root = new TableRoot
                 {
                     hardware_section,
                     password_setcion,
                     personal_section,
                     language_section
                 } 
             };
         
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = 10,
                    Children =
                    {
                        settings_table,
                        save_button
                    }
                }
            };
        }

        public void showLanguajesList()
        {
            Navigation.PushAsync(lg_lst);
        }
    }
}
