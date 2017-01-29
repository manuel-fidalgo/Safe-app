using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Safe
{
    class LanguagesList : ContentPage
    {

        //XML file with the settings
        public static readonly String[] LANGUAGES_CODE = new String[] { "EN", "ES", "IT", "DE", "FR" };
        public static readonly String[] LANGUAGES = new String[] { "  English", "  Spanish", "  Italian", "  Germany", "  French" };

        Color textColor = Color.FromRgb(165, 167, 159);

        public LanguagesList()
        {



            var english_cell = new TextCell()
            {
                Text = LANGUAGES[0],
                TextColor = textColor
            };
            english_cell.Tapped += English_cell_Tapped;

            var spanish_cell = new TextCell()
            {
                Text = LANGUAGES[1],
                TextColor = textColor
            };
            spanish_cell.Tapped += Spanish_cell_Tapped;


            var italian_cell = new TextCell()
            {
                Text = LANGUAGES[2],
                TextColor = textColor
            };
            italian_cell.Tapped += Italian_cell_Tapped;

            var german_cell = new TextCell()
            {
                Text = LANGUAGES[3],
                TextColor = textColor
            };
            german_cell.Tapped += German_cell_Tapped;

            var french_cell = new TextCell()
            {
                Text = LANGUAGES[4],
                TextColor = textColor
            };
            french_cell.Tapped += French_cell_Tapped;


            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new TableView {
                            Root = new TableRoot {
                                new TableSection("Choose language") {
                                    english_cell,
                                    spanish_cell,
                                    italian_cell,
                                    german_cell,
                                    french_cell,
                                }
                            }
                        }
                    }
                }
            };
        }

        //Tapped events for each cell
        private void English_cell_Tapped(object sender, EventArgs e)
        {
            changeLanguage(0);
        }
        private void Spanish_cell_Tapped(object sender, EventArgs e)
        {
            changeLanguage(1);
        }
        private void Italian_cell_Tapped(object sender, EventArgs e)
        {
            changeLanguage(2);
        }
        private void German_cell_Tapped(object sender, EventArgs e)
        {
            changeLanguage(3);
        }
        private void French_cell_Tapped(object sender, EventArgs e)
        {
            changeLanguage(4);
        }

        //Edits the XML file with the current language
        private void changeLanguage(int index)
        {
            SettingsPage.current_language = LANGUAGES[index];
            SettingsPage.current_language_code = LANGUAGES_CODE[index];

            Navigation.PopAsync();
        }
    }
}
