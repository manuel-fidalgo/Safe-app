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
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Button(),
                        new Button(),
                        new Button(),
                        new Button(),
                        new Button(),
                        new Button(),
                        new Button()
                        {
                            Text = "End"
                        }
                    }
                }
            };
        }
    }
}
