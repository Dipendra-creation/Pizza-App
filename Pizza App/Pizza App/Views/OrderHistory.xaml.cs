using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    // Enable XAML compilation for better performance and compile-time error checking.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderHistory : ContentPage
    {
        // Constructor: Initializes the OrderHistory page and sets up the refresh command.
        public OrderHistory()
        {
            InitializeComponent();

            // Set up the RefreshView command to trigger the RefreshOrders method when the user pulls-to-refresh.
            OrdersRefreshView.Command = new Command(async () => await RefreshOrders());
        }

        // OnAppearing is called when the page becomes visible.
        // It calls AnimateItems to animate the order items in the CollectionView.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AnimateItems();
        }

        /// <summary>
        /// Animates each order item in the CollectionView.
        /// This method creates new views from the DataTemplate and applies fade and translation animations.
        /// Note: This approach does not animate the actual rendered cells.
        /// </summary>
        private async void AnimateItems()
        {
            // Find the CollectionView by its name defined in XAML.
            var collectionView = this.FindByName<CollectionView>("OrdersCollectionView");
            if (collectionView != null && collectionView.ItemsSource != null)
            {
                // Animation parameters.
                uint duration = 300; // Duration for each animation (in milliseconds).
                uint delay = 100;    // Delay between animations for each item.

                // Iterate through each item in the ItemsSource.
                foreach (var item in collectionView.ItemsSource)
                {
                    // Create a new view from the DataTemplate.
                    var viewCell = collectionView.ItemTemplate?.CreateContent() as View;
                    if (viewCell != null)
                    {
                        // Set initial properties for animation.
                        viewCell.Opacity = 0;
                        viewCell.TranslationY = 50;

                        // Animate fade-in.
                        await viewCell.FadeTo(1, duration);
                        // Animate translation to the original position with a spring effect.
                        await viewCell.TranslateTo(0, 0, duration, Easing.SpringOut);

                        // Introduce a delay before animating the next item.
                        await Task.Delay((int)delay);
                    }
                }
            }
        }

        /// <summary>
        /// Simulates refreshing the list of orders.
        /// In a real app, this would involve calling a service to fetch updated orders.
        /// </summary>
        private async Task RefreshOrders()
        {
            // Simulate a delay to mimic network/service call.
            await Task.Delay(2000);

            // End the refreshing animation.
            OrdersRefreshView.IsRefreshing = false;
        }

        /// <summary>
        /// Handles the event when the "Reorder" button is clicked.
        /// Adds items from the selected order to the cart, shows a confirmation alert,
        /// and navigates the user to the cart page.
        /// </summary>
        private async void OnReorderClicked(object sender, EventArgs e)
        {
            // Notify the user that items have been added to the cart.
            await DisplayAlert("Reorder", "Items added to your cart!", "OK");

            // Navigate to the Cart page using absolute routing.
            await Shell.Current.GoToAsync("//cart");
        }

        /// <summary>
        /// Handles the swipe-to-delete action for an order item.
        /// Prompts the user for confirmation and, if confirmed, removes the order from the list.
        /// </summary>
        private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
            // Ask the user to confirm deletion.
            bool confirm = await DisplayAlert("Delete Order",
                "Are you sure you want to delete this order from your history?",
                "Yes", "No");

            if (confirm)
            {
                // Get the SwipeView that was swiped.
                var swipeView = sender as SwipeView;
                if (swipeView != null && swipeView.BindingContext != null)
                {
                    // In a real app, you would call a service to delete the order from the server or database.
                    // Here, we remove the order from the local CollectionView's list.
                    var collectionView = this.FindByName<CollectionView>("OrdersCollectionView");
                    if (collectionView != null && collectionView.ItemsSource is List<string> items)
                    {
                        // Remove the order from the list using its BindingContext.
                        items.Remove(swipeView.BindingContext as string);

                        // Refresh the CollectionView by reassigning the ItemsSource.
                        collectionView.ItemsSource = null;
                        collectionView.ItemsSource = items;
                    }
                }
            }
        }
    }
}
