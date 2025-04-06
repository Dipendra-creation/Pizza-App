using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Pizza_App.ViewModels;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Checkout : ContentPage
    {
        public Checkout()
        {
            InitializeComponent();
            // Set the BindingContext to the CheckoutViewModel to enable data binding.
            BindingContext = new CheckoutViewModel();

            // Initialize the map control with a default region and pin.
            InitializeMap();
        }

        /// <summary>
        /// Initializes the DeliveryMap control with a default region and a pin.
        /// The default location is set to New York City.
        /// </summary>
        private void InitializeMap()
        {
            // Define the default position (latitude, longitude)
            Position position = new Position(40.7128, -74.0060);
            // Create a map span with a 1-mile radius around the position
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1));
            // Move the map to the defined region
            DeliveryMap.MoveToRegion(mapSpan);

            // Create a pin representing the delivery location
            var pin = new Pin
            {
                Label = "Delivery Location",
                Address = "123 Main St, New York, NY 10001",
                Type = PinType.Place,
                Position = position
            };

            // Add the pin to the map's Pins collection
            DeliveryMap.Pins.Add(pin);
        }
    }
}
