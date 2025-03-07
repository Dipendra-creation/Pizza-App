using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderHistory : ContentPage
    {
        public OrderHistory()
        {
            InitializeComponent();

            // Set up the RefreshView command
            OrdersRefreshView.Command = new Command(async () => await RefreshOrders());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Animate the order items when the page appears
            AnimateItems();
        }

        private async void AnimateItems()
        {
            // Attempt to find the CollectionView named "OrdersCollectionView"
            var collectionView = this.FindByName<CollectionView>("OrdersCollectionView");
            if (collectionView != null && collectionView.ItemsSource != null)
            {
                uint duration = 300;
                uint delay = 100;

                // For each item in the ItemsSource, create the view from the DataTemplate and animate it.
                // Note: This naive approach doesn't animate the *actual* rendered cells, just newly created ones.
                foreach (var item in collectionView.ItemsSource)
                {
                    var viewCell = collectionView.ItemTemplate?.CreateContent() as View;
                    if (viewCell != null)
                    {
                        viewCell.Opacity = 0;
                        viewCell.TranslationY = 50;

                        await viewCell.FadeTo(1, duration);
                        await viewCell.TranslateTo(0, 0, duration, Easing.SpringOut);

                        await Task.Delay((int)delay);
                    }
                }
            }
        }

        private async Task RefreshOrders()
        {
            // Simulate refreshing orders (e.g., call a service)
            await Task.Delay(2000);

            // End the refresh
            OrdersRefreshView.IsRefreshing = false;
        }

        private async void OnReorderClicked(object sender, EventArgs e)
        {
            // Add items from this order to the cart
            await DisplayAlert("Reorder", "Items added to your cart!", "OK");

            // Navigate to the cart page
            await Shell.Current.GoToAsync("//cart");
        }

        private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
            // Ask for confirmation before deleting
            bool confirm = await DisplayAlert("Delete Order",
                "Are you sure you want to delete this order from your history?",
                "Yes", "No");

            if (confirm)
            {
                // Remove the order from the list
                var swipeView = sender as SwipeView;
                if (swipeView != null && swipeView.BindingContext != null)
                {
                    // In a real app, you'd call a service to delete the order from the server/database
                    // For now, we just remove it from the CollectionView's local list
                    var collectionView = this.FindByName<CollectionView>("OrdersCollectionView");
                    if (collectionView != null && collectionView.ItemsSource is List<string> items)
                    {
                        items.Remove(swipeView.BindingContext as string);

                        // Refresh the CollectionView
                        collectionView.ItemsSource = null;
                        collectionView.ItemsSource = items;
                    }
                }
            }
        }
    }
}
