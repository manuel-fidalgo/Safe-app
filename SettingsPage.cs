using System;
using Xamarin.Forms;

namespace Safe
{
    class SettingsPage : ContentPage
    {

        LanguagesList lg_lst;
        SwitchCell gps_cell, accelerometer_cell, vibration_cell, password_cell;
        EntryCell password_text, contact_message, contact_number, emergency_number;
        TableSection hardware_section, password_setcion, personal_section, language_section;
        LanguageCell languagecell;

        public SettingsPage()
        {

            SettingsWrap.loadSettings();
            lg_lst = new LanguagesList();

            //Hardware config

            gps_cell = new SwitchCell
            {
                Text = "GPS detection"
            };
            

            accelerometer_cell = new SwitchCell
            {
                Text = "Accelerometer detection"
            };

            vibration_cell = new SwitchCell
            {
                Text = "Vibration"
            };

            hardware_section = new TableSection("Hardware Config")
            {
                gps_cell,
                accelerometer_cell,
                vibration_cell,
            };

            //Password configuration

            password_cell = new SwitchCell
            {
                Text = "Use a Security code",
                On = false
            };
            password_cell.OnChanged += Password_cell_OnChanged;

            password_text = new EntryCell
            {
                Label = "Security code",
                Placeholder = "****",
                IsEnabled = false
            };

            password_setcion = new TableSection("Password config")
            {
                password_cell,
                password_text
            };

            //Personal data configuration

            contact_message = new EntryCell
            {
                Label = "Conctact Message:  ",
            };

            contact_number = new EntryCell
            {
                Label = "Contact number:  ",
            };

            emergency_number = new EntryCell
            {
                Label = "Emergency number  ",
                LabelColor = Color.FromRgb(255, 100, 100),
                Placeholder = "112"
            };

            personal_section = new TableSection("Personal info")
            {
                contact_message,
                contact_number,
                emergency_number
            };

            //Language cell

            languagecell = new LanguageCell(this);

            language_section = new TableSection("Language")
            {
                languagecell
            };

            //Save button

            var save_button = new Button
            {
                Text = "Save",
                TextColor = Color.Aqua
            };
            save_button.Clicked += Save_button_Clicked;

            var save_cell = new ViewCell
            {
                View = save_button
            };

            var save_section = new TableSection("Save settings")
            {
                save_cell
            };


            var settings_table = new TableView
            {
                Root = new TableRoot
                 {
                    hardware_section,
                    password_setcion,
                    personal_section,
                    language_section,
                    save_section
                    
                 },
                Intent = TableIntent.Settings
            };

            Content = new ScrollView
            {
                Content = settings_table,
                IsClippedToBounds = true
            };

        }

        private void Password_cell_OnChanged(object sender, ToggledEventArgs e)
        {
            if (password_cell.On)
                password_text.IsEnabled = true;
            else
                password_text.IsEnabled = false;
        }

        private void Save_button_Clicked(object sender, EventArgs e)
        {
            saveSettings();
            Navigation.PopAsync();
        }

        public void showLanguajesList()
        {
            Navigation.PushAsync(lg_lst);
        }

        public void saveSettings()
        {

        }
    }
}
