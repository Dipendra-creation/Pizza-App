using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Services;

namespace Pizza_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Initialize the SQLite database (create tables if they don't exist)
            InitializeDatabaseAsync();

            // Set the main page to AppShell for navigation management
            MainPage = new AppShell();
        }

        // Asynchronously initializes the SQLite database
        private async void InitializeDatabaseAsync()
        {
            await SQLiteService.InitializeAsync();
        }

        protected override void OnStart()
        {
            // Handle any startup tasks here
        }

        protected override void OnSleep()
        {
            // Handle when your app goes to sleep (e.g., save state)
        }

        protected override void OnResume()
        {
            // Handle when your app resumes (e.g., restore state)
        }
    }
}
