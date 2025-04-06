using System;
using System.Threading.Tasks;
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

            // Initialize the SQLite database and seed initial data.
            InitializeDatabaseAsync();

            // Always use AppShell as the MainPage for unified navigation.
            MainPage = new AppShell();

            // Check if the user is logged in using Preferences.
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);
            if (!isLoggedIn)
            {
                // Navigate to the Login page using Shell's routing.
                // This assumes that the "login" route is registered in AppShell.xaml.cs.
                Shell.Current.GoToAsync("login");
            }
        }

        // Asynchronously initializes the SQLite database and seeds initial data.
        private async void InitializeDatabaseAsync()
        {
            await SQLiteService.InitializeAsync();
            await SeedPizzaDataAsync();
        }

        // Seeds the Pizza table with initial data if it's empty.
        private async Task SeedPizzaDataAsync()
        {
            var pizzaService = new PizzaService();
            var existingPizzas = await pizzaService.GetPizzasAsync();
            if (existingPizzas.Count == 0)
            {
                var pizzas = new System.Collections.Generic.List<Models.Pizza>
                {
                    new Models.Pizza { Id = 1, Name = "Margherita", Price = 10.99m, Description = "Classic pizza with tomato, mozzarella, and basil", ImageName = "pizza_margherita.jpg" },
                    new Models.Pizza { Id = 2, Name = "Supreme", Price = 12.99m, Description = "Loaded with toppings", ImageName = "pizza_supreme.jpg" },
                    new Models.Pizza { Id = 3, Name = "Hawaiian", Price = 11.99m, Description = "Ham and pineapple", ImageName = "pizza_hawaiian.jpg" },
                    new Models.Pizza { Id = 4, Name = "BBQ Chicken", Price = 13.99m, Description = "BBQ sauce with chicken", ImageName = "pizza_bbqchicken.jpg" },
                    new Models.Pizza { Id = 5, Name = "Pepperoni", Price = 12.99m, Description = "Pepperoni pizza", ImageName = "pizza_pepperoni.jpg" },
                    new Models.Pizza { Id = 6, Name = "Veggie Delight", Price = 11.49m, Description = "Loaded with fresh vegetables", ImageName = "pizza_veggiedelight.jpg" },
                    new Models.Pizza { Id = 7, Name = "Meat Lovers", Price = 13.49m, Description = "A hearty mix of various meats", ImageName = "pizza_meatlovers.jpg" },
                    new Models.Pizza { Id = 8, Name = "Buffalo Chicken", Price = 14.49m, Description = "Spicy buffalo chicken pizza", ImageName = "pizza_buffalochicken.jpg" },
                    new Models.Pizza { Id = 9, Name = "Four Cheese", Price = 12.49m, Description = "A blend of four delicious cheeses", ImageName = "pizza_fourcheese.jpg" },
                    new Models.Pizza { Id = 10, Name = "Mediterranean", Price = 13.99m, Description = "With olives, feta, and sun-dried tomatoes", ImageName = "pizza_mediterranean.jpg" },
                    new Models.Pizza { Id = 11, Name = "Pesto Chicken", Price = 14.99m, Description = "Pizza with a basil pesto sauce and chicken", ImageName = "pizza_pestochicken.jpg" },
                    new Models.Pizza { Id = 12, Name = "Spinach Alfredo", Price = 13.49m, Description = "Creamy alfredo sauce with fresh spinach", ImageName = "pizza_spinachalfredo.jpg" },
                    new Models.Pizza { Id = 13, Name = "BBQ Pulled Pork", Price = 15.49m, Description = "Topped with tender pulled pork in BBQ sauce", ImageName = "pizza_bbqpulledpork.jpg" },
                    new Models.Pizza { Id = 14, Name = "Mexican Fiesta", Price = 14.49m, Description = "Spicy salsa, jalapeños, and guacamole", ImageName = "pizza_mexicanfiesta.jpg" },
                    new Models.Pizza { Id = 15, Name = "Truffle Mushroom", Price = 16.99m, Description = "Wild mushrooms with a hint of truffle oil", ImageName = "pizza_trufflemushroom.jpg" }
                };

                await pizzaService.AddPizzasAsync(pizzas);
            }
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
