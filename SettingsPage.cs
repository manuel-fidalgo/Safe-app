using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Safe
{
    class SettingsPage : ContentPage
    {
        public SettingsPage()
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
                        new Button(),
                        new Button(),
                        new Button(),
                        new Button(),
                    }
                }
            };
        }
    }
}
