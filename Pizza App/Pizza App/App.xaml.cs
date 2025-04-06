using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Services;
using Pizza_App.Views;

namespace Pizza_App
{
    public partial class App : Application
    {
        // This constructor runs when the app starts up.
        // It initializes resources and sets the main page.
        public App()
        {
            InitializeComponent();


            // Set the main page to AppShell, which handles our app's navigation.
            MainPage = new AppShell();
        }

        // This method is called when the app is first launched or resumed from not running.
        protected override void OnStart()
        {
            // Perform any startup tasks here, like checking for updates or setting analytics.
        }

        // This method is called when the app goes into the background (e.g., user switches away).
        protected override void OnSleep()
        {
            // Use this for saving data or releasing resources you don’t need in the background.
        }

        // This method is called when the app returns from the background to the foreground.
        protected override void OnResume()
        {
            // Restore any data or state that was released or saved during OnSleep.
        }
    }
}
