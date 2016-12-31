using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Safe
{
    //wraps the main page into a navigation page
    public class PageManager
    {
        public static NavigationPage Init()
        {
            NavigationPage nav = new NavigationPage(new MainPage());
            return nav;
        }
    }
}
