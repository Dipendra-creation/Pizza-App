using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    // Enable XAML compilation for performance improvements and error checking.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Checkout : ContentPage
    {
        // Constructor: Initializes the Checkout page and its components.
        public Checkout()
        {
            InitializeComponent();
            // Initialize the map control with a default location.
            InitializeMap();
        }

        /// <summary>
        /// Initializes the DeliveryMap with a default region and pin.
        /// This example uses New York City as the default position.
        /// </summary>
        private void InitializeMap()
        {
            // Define the default position (latitude, longitude).
            Position position = new Position(40.7128, -74.0060);
            // Create a map span around the position with a 1-mile radius.
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1));
            // Move the map to the specified region.
            DeliveryMap.MoveToRegion(mapSpan);

            // Create a pin representing the delivery location.
            var pin = new Pin
            {
                Label = "Delivery Location",
                Address = "123 Main St, New York, NY 10001",
                Type = PinType.Place,
                Position = position
            };
            // Add the pin to the map's Pins collection.
            DeliveryMap.Pins.Add(pin);
        }

        /// <summary>
        /// Event handler for when the "Place Order" button is clicked.
        /// Simulates order processing, displays an activity indicator, and navigates back to the home page.
        /// </summary>
        private async void OnPlaceOrderClicked(object sender, EventArgs e)
        {
            // Display the activity indicator to signal order processing.
            OrderActivityIndicator.IsVisible = true;
            OrderActivityIndicator.IsRunning = true;

            // Simulate a delay to represent order processing (e.g., network request).
            await Task.Delay(2000);

            // Hide the activity indicator after processing.
            OrderActivityIndicator.IsVisible = false;
            OrderActivityIndicator.IsRunning = false;

            // Display a success alert to the user.
            await DisplayAlert("Order Placed",
                "Your order has been placed successfully! You will receive a confirmation shortly.",
                "OK");

            // Navigate back to the home page using an absolute routing scheme.
            await Shell.Current.GoToAsync("//home");
        }
    }
}
