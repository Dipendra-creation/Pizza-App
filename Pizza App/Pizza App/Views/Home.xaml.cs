using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    // Enable XAML compilation for improved performance and error checking.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        // Constructor: Initializes the Home page and its components.
        public Home()
        {
            InitializeComponent();
        }

        // Override OnAppearing to perform actions each time the page appears.
        protected override void OnAppearing()
        {
            // Call the base method to ensure default behavior.
            base.OnAppearing();
            // Trigger the animation for items on the page.
            AnimateItems();
        }

        // Method to animate items in the CollectionView.
        // This method performs a sequential fade and translate animation on each item.
        private async void AnimateItems()
        {
            // Find the CollectionView by its name defined in XAML.
            var pizzaCollectionView = this.FindByName<CollectionView>("PizzaCollectionView");
            // If the CollectionView or its ItemsSource is null, exit the method.
            if (pizzaCollectionView == null || pizzaCollectionView.ItemsSource == null)
                return;

            // Define animation timing parameters.
            uint duration = 300; // Duration for each animation (in milliseconds).
            uint delay = 100;    // Delay between animations of each item.

            // Cast the ItemsSource to an IEnumerable to iterate over its items.
            var items = pizzaCollectionView.ItemsSource as System.Collections.IEnumerable;
            if (items == null)
                return;

            // Iterate over each item in the CollectionView.
            // Note: This naive approach creates new View instances from the DataTemplate 
            // instead of animating the actual rendered cells.
            foreach (var item in items)
            {
                // Create a new view instance for the item using the DataTemplate.
                var viewCell = pizzaCollectionView.ItemTemplate?.CreateContent() as View;
                if (viewCell != null)
                {
                    // Initialize the view's opacity and vertical translation.
                    viewCell.Opacity = 0;
                    viewCell.TranslationY = 50;

                    // Animate the view to fade in.
                    await viewCell.FadeTo(1, duration);
                    // Animate the view to move into its final position with a spring effect.
                    await viewCell.TranslateTo(0, 0, duration, Easing.SpringOut);

                    // Introduce a delay before animating the next item.
                    await Task.Delay((int)delay);
                }
            }
        }

        // Event handler for when a pizza item is tapped.
        // Navigates to the pizza customization page, passing a sample pizzaId.
        private async void OnPizzaTapped(object sender, EventArgs e)
        {
            // Navigate to the "customization" route with a sample pizzaId query parameter.
            await Shell.Current.GoToAsync("///customization?pizzaId=1");
        }
    }
}
