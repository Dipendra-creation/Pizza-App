using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Models;
using Pizza_App.Services;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Home page.
    public class HomeViewModel : BaseViewModel
    {
        private readonly PizzaService _pizzaService;
        private readonly CartService _cartService;

        public ObservableCollection<Pizza> Pizzas { get; } = new ObservableCollection<Pizza>();
        public ObservableCollection<Pizza> RecommendedPizzas { get; } = new ObservableCollection<Pizza>();

        public ICommand AddToCartCommand { get; }

        public HomeViewModel()
        {
            _pizzaService = new PizzaService();
            _cartService = new CartService();

            AddToCartCommand = new Command<Pizza>(async (pizza) => await ExecuteAddToCartCommand(pizza));

            Task.Run(async () => await LoadPizzasAsync());
        }

        private async Task LoadPizzasAsync()
        {
            try
            {
                var pizzas = await _pizzaService.GetAllPizzasAsync();

                Device.BeginInvokeOnMainThread(() =>
                {
                    Pizzas.Clear();
                    RecommendedPizzas.Clear();

                    foreach (var pizza in pizzas)
                    {
                        Pizzas.Add(pizza);

                        // Simple recommendation logic
                        if (pizza.Name.Contains("Delight") || pizza.Name.Contains("Feast") || pizza.Name.Contains("Buffalo"))
                        {
                            RecommendedPizzas.Add(pizza);
                        }
                    }
                });
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load pizzas.", "OK");
            }
        }

        private async Task ExecuteAddToCartCommand(Pizza pizza)
        {
            if (pizza == null)
                return;

            var cartItem = new CartItem
            {
                PizzaName = pizza.Name,
                UnitPrice = pizza.Price,
                Quantity = 1,
                TotalPrice = pizza.Price,
                Size = "Medium",
                Crust = "Thin",
                Toppings = ""
            };

            await _cartService.AddCartItemAsync(cartItem);

            await Application.Current.MainPage.DisplayAlert("Success", $"{pizza.Name} added to cart.", "OK");
        }
    }
}
