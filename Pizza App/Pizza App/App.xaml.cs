using System;
using System.Threading.Tasks;
using Xamarin.Essentials; // For Preferences
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

            // Set the main page to AppShell for unified navigation
            MainPage = new AppShell();

            // Initialize the SQLite database and seed data
            InitializeAppAsync();
        }

        private async void InitializeAppAsync()
        {
            await SQLiteService.InitializeAsync();

            // Seed pizzas if not already seeded
            var pizzaService = new PizzaService();
            await pizzaService.SeedDefaultPizzasAsync();

            // Redirect to login if not logged in
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);
            if (!isLoggedIn)
            {
                await Shell.Current.GoToAsync("//login");
            }
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
