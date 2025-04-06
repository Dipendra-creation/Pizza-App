using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Models;
using Pizza_App.Services;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Home page.
    // Loads pizzas from the local database and handles adding a pizza to the cart.
    public class HomeViewModel : BaseViewModel
    {
        private readonly PizzaService pizzaService;
        private readonly CartService cartService;

        // Collection of all pizzas.
        public ObservableCollection<Pizza> Pizzas { get; } = new ObservableCollection<Pizza>();
        // Collection of recommended pizzas.
        public ObservableCollection<Pizza> RecommendedPizzas { get; } = new ObservableCollection<Pizza>();

        // Command to add a selected pizza to the cart.
        public ICommand AddToCartCommand { get; }

        public HomeViewModel()
        {
            pizzaService = new PizzaService();
            cartService = new CartService();
            AddToCartCommand = new Command<Pizza>(async (pizza) => await ExecuteAddToCartCommand(pizza));

            // Load pizzas from the local database.
            Task.Run(async () => await LoadPizzasAsync());
        }

        // Loads pizzas from the local database.
        private async Task LoadPizzasAsync()
        {
            var pizzas = await pizzaService.GetPizzasAsync();
            Pizzas.Clear();
            foreach (var pizza in pizzas)
            {
                Pizzas.Add(pizza);
            }

            // For demonstration, mark some pizzas as recommended (e.g., by filtering on name)
            RecommendedPizzas.Clear();
            foreach (var pizza in pizzas)
            {
                if (pizza.Name.Contains("Delight") || pizza.Name.Contains("Feast") || pizza.Name.Contains("Buffalo"))
                {
                    RecommendedPizzas.Add(pizza);
                }
            }
        }

        // Adds the selected pizza to the cart.
        private async Task ExecuteAddToCartCommand(Pizza pizza)
        {
            if (pizza == null)
                return;

            // Create a new CartItem using pizza details.
            var cartItem = new CartItem
            {
                PizzaName = pizza.Name,
                UnitPrice = pizza.Price,
                Quantity = 1,
                TotalPrice = pizza.Price,
                Size = "Medium",  // Default value; may be customized later.
                Crust = "Thin",   // Default value; may be customized later.
                Toppings = ""     // Default; updated later in customization.
            };

            // Add the cart item to the database.
            await cartService.AddCartItemAsync(cartItem);

            // Provide user feedback.
            await Application.Current.MainPage.DisplayAlert("Success", $"{pizza.Name} added to cart.", "OK");
        }
    }
}
