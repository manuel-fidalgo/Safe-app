using System;
using Xamarin.Forms;

namespace Safe
{
    class SettingsPage : ContentPage
    {

        LanguagesList lg_lst;
        
        SwitchCell gps_cell, accelerometer_cell, vibration_cell, password_cell;
        EntryCell password_text, contact_message, contact_number, emergency_number;
        TableSection hardware_section, password_setcion, personal_section, language_section, test_section;
        TextCell test_cell,map_cell,languagecell;
        public static string current_language;
        public static string current_language_code;

        Color textColor = Color.FromRgb(165, 167, 159);

        MapPage map_page;
        HardwarePage hardware_page;

        public SettingsPage()
        {
            SettingsWrap.loadSettingsfromXML();

            //Create the two subpages
            map_page = new MapPage();    //BUG IN MAP PAGE!
            hardware_page = new HardwarePage();

            //Create all the items in the settings menu
            test_cell = new TextCell()
            {
                Text = "Display Test Page",
                TextColor = textColor
            };
            test_cell.Tapped += Test_cell_Tapped;

            map_cell = new TextCell()
            {
                Text = "Show Map",
                TextColor = textColor
            };
            map_cell.Tapped += Map_cell_Tapped;
            
            test_section = new TableSection("Testing")
            {
                test_cell,
                map_cell
            };

           
            lg_lst = new LanguagesList();

            //Hardware config

            gps_cell = new SwitchCell
            {
                Text = AppResources.gps_status,
            };
            

            accelerometer_cell = new SwitchCell
            {
                Text = AppResources.accelerometer_status
            };

            vibration_cell = new SwitchCell
            {
                Text = AppResources.vibration_status
            };

            hardware_section = new TableSection(AppResources.hardware_config)
            {
                gps_cell,
                accelerometer_cell,
                vibration_cell,
            };

            //Password configuration

            password_cell = new SwitchCell
            {
                Text = AppResources.use_pin,
                On = false
            };
            password_cell.OnChanged += Password_cell_OnChanged;

            password_text = new EntryCell
            {
                Label = AppResources.pin_code,
                Placeholder = "****",
                IsEnabled = false
            };

            password_setcion = new TableSection(AppResources.password_config)
            {
                password_cell,
                password_text
            };

            //Personal data configuration

            contact_message = new EntryCell
            {
                Label = AppResources.contact_message,
                LabelColor = textColor
            };

            contact_number = new EntryCell
            {
                Label = AppResources.contact_number,
                LabelColor = textColor
            };

            emergency_number = new EntryCell
            {
                Label = AppResources.contact_number,
                LabelColor = Color.FromRgb(255, 100, 100),
                Placeholder = "112"
            };

            personal_section = new TableSection(AppResources.personal_info_config)
            {
                contact_message,
                contact_number,
                emergency_number
            };

            //Language cell

            languagecell = new TextCell()
            {
                Text = "Chooose language",
                TextColor = textColor
            };
            languagecell.Tapped += Languagecell_Tapped;

            language_section = new TableSection(AppResources.language)
            {
                languagecell
            };

            //Save cell

            var save_cell = new TextCell()
            {
                Text = "Save Settings",
                TextColor = textColor
            };
            save_cell.Tapped += Save_button_Clicked;

            var save_section = new TableSection(AppResources.save_settings)
            {
                save_cell
            };


            var settings_table = new TableView
            {
                Root = new TableRoot
                 {
                    test_section,
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

       
        private void Languagecell_Tapped(object sender, EventArgs e)
        {
            showLanguajesList();
        }

        private void Map_cell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(map_page);
        }

        private void Test_cell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(hardware_page);
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
            SettingsWrap.set_gps_status(gps_cell.On);
            SettingsWrap.set_accelerometer_status(accelerometer_cell.On);
            SettingsWrap.set_vibration_status(vibration_cell.On);

            SettingsWrap.set_seccode_status(password_cell.On);

            if (password_cell.On)
                SettingsWrap.set_securityCode(password_text.Text);
            else
                SettingsWrap.set_securityCode(SettingsWrap.DEFAULT_PASS);

            SettingsWrap.setContactMessage(contact_message.Text);
            SettingsWrap.setContactNumber(contact_number.Text);
            SettingsWrap.setEmergencyNumber(emergency_number.Text);

            SettingsWrap.writeSettingsintoXML();
        }
    }
}
