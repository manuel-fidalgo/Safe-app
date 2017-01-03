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

        //Language Codes  (ISO 639-1 codes)

        public static readonly String SPANISH = "es";
        public static readonly String ENGLISH = "en";
        public static readonly String ITALIAN = "it";
        public static readonly String GERMANY = "gr";
        public static readonly String FRENCH = "fr";

        //XML file with the settings

       
        public LanguagesList()
        {


            var english_buttom = new Button
            {
                Text = "English",
            };
            english_buttom.Clicked += English_buttom_Clicked;

            var spanish_buttom = new Button
            {
                Text = "Spanish"
            };
            spanish_buttom.Clicked += Spanish_buttom_Clicked;

            var italian_buttom = new Button
            {
                Text = "Italian"
            };
            italian_buttom.Clicked += Italian_buttom_Clicked;

            var german_buttom = new Button
            {
                Text = "German"
            };
            german_buttom.Clicked += Germany_buttom_Clicked;

            var french_buttom = new Button
            {
                Text = "French"
            };
            french_buttom.Clicked += French_buttom_Clicked;


            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new TableView {
                            Root = new TableRoot {
                                new TableSection("Choose language") {
                                    new ViewCell {View = english_buttom},
                                    new ViewCell {View = spanish_buttom},
                                    new ViewCell {View = italian_buttom },
                                    new ViewCell {View = german_buttom },
                                    new ViewCell {View = english_buttom },
                                    new ViewCell {View = french_buttom }
                                }
                            }
                        }
                    }
                }
            };
        }

        private void French_buttom_Clicked(object sender, EventArgs e)
        {
            changeLanguage(FRENCH);
        }

        private void Germany_buttom_Clicked(object sender, EventArgs e)
        {
            changeLanguage(GERMANY);
        }

        private void Italian_buttom_Clicked(object sender, EventArgs e)
        {
            changeLanguage(ITALIAN);
        }

        private void Spanish_buttom_Clicked(object sender, EventArgs e)
        {
            changeLanguage(SPANISH);
        }

        private void English_buttom_Clicked(object sender, EventArgs e)
        {
            changeLanguage(ENGLISH);
        }

        //Edits the XML file with the current language
        private void changeLanguage(string len)
        {
            Navigation.PopAsync();
        }
    }
}
