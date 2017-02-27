using Android.OS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;




namespace Safe 
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            
            MainPage = PageManager.Init();
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private interface ILocalize
        {
            CultureInfo GetCurrentCultureInfo();
            void SetLocale(CultureInfo ci);
        }
    }
}
