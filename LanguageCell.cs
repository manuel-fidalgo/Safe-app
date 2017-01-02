using System;
using Xamarin.Forms;


namespace Safe
{
    class LanguageCell : ViewCell
    {

        SettingsPage settings_page;

       
        public LanguageCell(SettingsPage stt_pg)
        {
            settings_page = stt_pg;
            setUpEvent();
            View = new Label()
            {
                Text = "\n"+"Language: Current Language"
            };
            
        }
        public void setUpEvent()
        {
            Tapped += tappedEvent;
        }

        public void tappedEvent(object sender, EventArgs args)
        {
            settings_page.showLanguajesList();
        }
    }
}

/**
ArrayAdapter lol = new ArrayAdapter(this,"What is this number? xD");
    lol.Add("lol");
    spinner.Adapter = lol;
 */
