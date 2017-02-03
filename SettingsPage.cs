using System;
using Xamarin.Forms;

namespace Safe
{
    class SettingsPage : ContentPage
    {

        LanguagesList lg_lst;
        
        SwitchCell detect_crash, detect_falls, vibration_cell, password_cell, use_access;
        EntryCell password_text, contact_message, contact_number;
        TableSection hardware_section, password_setcion, personal_section, language_section, test_section;
        TextCell test_cell,map_cell,languagecell;
        public static string current_language;
        public static string current_language_code;

        Color textColor = Color.FromRgb(165, 167, 159);

        MapPage map_page;
        HardwarePage hardware_page;

        public SettingsPage()
        {
            SettingsWrap.loadSettingsfromFile();

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

            detect_crash = new SwitchCell
            {
                Text = AppResources.crash_status,
                On = SettingsWrap.crash_status
            };
            

            detect_falls = new SwitchCell
            {
                Text = AppResources.falls_status,
                On = SettingsWrap.falls_status
            };

            vibration_cell = new SwitchCell
            {
                Text = AppResources.vibration_status,
                On = SettingsWrap.vibration_status
            };

            hardware_section = new TableSection(AppResources.hardware_config)
            {
                detect_crash,
                detect_falls,
                vibration_cell,
            };

            //Password configuration

            password_cell = new SwitchCell
            {
                Text = AppResources.use_pin,
                On = SettingsWrap.security_code_status
            };
            password_cell.OnChanged += Password_cell_OnChanged;

            password_text = new EntryCell
            {
                Label = AppResources.pin_code,
                Placeholder = SettingsWrap.seccurity_code,
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
                Placeholder = SettingsWrap.contact_Message,
                LabelColor = textColor
            };

            contact_number = new EntryCell
            {
                Label = AppResources.contact_number,
                Placeholder = SettingsWrap.contact_number,
                LabelColor = textColor
            };

            personal_section = new TableSection(AppResources.personal_info_config)
            {
                contact_message,
                contact_number,
            };

            //Language cell

            languagecell = new TextCell()
            {
                Text = "Language " + SettingsWrap.currentLanguage_code,
                TextColor = textColor
            };
            languagecell.Tapped += Languagecell_Tapped;

            language_section = new TableSection(AppResources.language)
            {
                languagecell
            };

            //Acess cell

            use_access = new SwitchCell()
            {
                Text = "Accesibility mode",
                On = SettingsWrap.use_accesibility
            };
            var access_section = new TableSection("Accesiblility")
            {
                use_access
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
                    access_section,
                    save_section,
                    
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
            SettingsWrap.writeSettingsintoFile();
        }
    }
}
