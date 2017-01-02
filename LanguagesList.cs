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

        public LanguagesList()
        {
            var english_buttom = new Button
            {
                Text = "Spanish",
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

            var germany_buttom = new Button
            {
                Text = "Germany"
            };
            germany_buttom.Clicked += Germany_buttom_Clicked;

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
                        new TableView { Root = new TableRoot { new TableSection("Choose language") }},
                        english_buttom,
                        spanish_buttom,
                        italian_buttom,
                        germany_buttom,
                        french_buttom
                    }
                }
            };
        }

        private void French_buttom_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Germany_buttom_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Italian_buttom_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Spanish_buttom_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void English_buttom_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
