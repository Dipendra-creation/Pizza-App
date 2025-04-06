using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Services;
using Xamarin.Essentials; // For Preferences

namespace Pizza_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Initialize the SQLite database (create tables if they don't exist)
            InitializeDatabaseAsync();

            // Check the login status using Preferences.
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);

            // Set the MainPage conditionally based on the login flag.
            if (!isLoggedIn)
            {
                // If the user is not logged in, display the Login page wrapped in a NavigationPage.
                MainPage = new NavigationPage(new Views.Login());
            }
            else
            {
                // If the user is logged in, load the AppShell for full navigation.
                MainPage = new AppShell();
            }
        }

        // Asynchronously initializes the SQLite database.
        private async void InitializeDatabaseAsync()
        {
            await SQLiteService.InitializeAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts.
        }

        protected override void OnSleep()
        {
            // Handle when your app goes to sleep.
        }

        protected override void OnResume()
        {
            // Handle when your app resumes.
        }
    }
}
