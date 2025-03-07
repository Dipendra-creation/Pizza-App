using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Checkout : ContentPage
    {
        public Checkout()
        {
            InitializeComponent();

            // Initialize the map with a default position
            InitializeMap();
        }

        private void InitializeMap()
        {
            // Example default location: New York City
            Position position = new Position(40.7128, -74.0060);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1));
            DeliveryMap.MoveToRegion(mapSpan);

            // Add a pin for the delivery location
            var pin = new Pin
            {
                Label = "Delivery Location",
                Address = "123 Main St, New York, NY 10001",
                Type = PinType.Place,
                Position = position
            };
            DeliveryMap.Pins.Add(pin);
        }

        private async void OnPlaceOrderClicked(object sender, EventArgs e)
        {
            // Show the activity indicator
            OrderActivityIndicator.IsVisible = true;
            OrderActivityIndicator.IsRunning = true;

            // Simulate order processing
            await Task.Delay(2000);

            // Hide the activity indicator
            OrderActivityIndicator.IsVisible = false;
            OrderActivityIndicator.IsRunning = false;

            // Show a success message
            await DisplayAlert("Order Placed",
                "Your order has been placed successfully! You will receive a confirmation shortly.",
                "OK");

            // Navigate back to the home page
            await Shell.Current.GoToAsync("//home");
        }
    }
}
