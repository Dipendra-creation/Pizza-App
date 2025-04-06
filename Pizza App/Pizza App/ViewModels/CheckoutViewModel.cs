using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Models;
using Pizza_App.Services;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Checkout page.
    // Handles user details and order placement.
    public class CheckoutViewModel : BaseViewModel
    {
        private readonly OrderService orderService;
        private readonly CartService cartService;

        // User's full name (can be pre-populated from profile)
        private string fullName;
        public string FullName
        {
            get => fullName;
            set { fullName = value; OnPropertyChanged(); }
        }

        // User's phone number
        private string phone;
        public string Phone
        {
            get => phone;
            set { phone = value; OnPropertyChanged(); }
        }

        // Delivery address
        private string address;
        public string Address
        {
            get => address;
            set { address = value; OnPropertyChanged(); }
        }

        // Total order price (computed from cart items)
        private decimal totalPrice;
        public decimal TotalPrice
        {
            get => totalPrice;
            set { totalPrice = value; OnPropertyChanged(); }
        }

        // Command to place the order.
        public ICommand PlaceOrderCommand { get; }

        public CheckoutViewModel()
        {
            orderService = new OrderService();
            cartService = new CartService();
            PlaceOrderCommand = new Command(async () => await ExecutePlaceOrderCommand());
        }

        // Executes the checkout process:
        // 1. Retrieves cart items and calculates the total price.
        // 2. Creates a new Order object and saves it to the database.
        // 3. Optionally clears the cart upon success.
        // 4. Navigates to the Order History page.
        private async Task ExecutePlaceOrderCommand()
        {
            // Retrieve all cart items.
            var cartItems = await cartService.GetCartItemsAsync();

            // Calculate subtotal from the cart items.
            decimal subtotal = 0;
            foreach (var item in cartItems)
            {
                subtotal += item.TotalPrice;
            }
            TotalPrice = subtotal; // Additional charges like tax or delivery fee can be added here.

            // Create an Order object.
            var order = new Order
            {
                // For demonstration, assigning a dummy user id.
                // In a real app, use the logged-in user's id.
                UserId = 1,
                TotalAmount = TotalPrice,
                Status = "In Progress",
                // Optionally serialize the cart items into OrderDetails (e.g., as JSON).
                OrderDetails = ""
            };

            // Save the order to the database.
            var result = await orderService.AddOrderAsync(order);
            if (result > 0)
            {
                // Optionally clear the cart after successful order placement.
                // (Implementation of cart clearance is not shown here.)
                await Application.Current.MainPage.DisplayAlert("Success", "Your order has been placed.", "OK");
                // Navigate to the Order History page.
                await Shell.Current.GoToAsync("///orderhistory");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "There was a problem placing your order.", "OK");
            }
        }
    }
}
