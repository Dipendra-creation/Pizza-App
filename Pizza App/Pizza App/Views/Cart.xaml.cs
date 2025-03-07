using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        public Cart()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Animate cart items (Frames) when the page appears
            AnimateItems();
        }

        private async void AnimateItems()
        {
            // Find the StackLayout named "CartItemsLayout"
            var stackLayout = this.FindByName<StackLayout>("CartItemsLayout");
            if (stackLayout != null)
            {
                uint duration = 300;
                uint delay = 100;

                // Animate each child if it's a Frame
                foreach (var item in stackLayout.Children)
                {
                    if (item is Frame itemFrame)
                    {
                        itemFrame.Opacity = 0;
                        itemFrame.TranslationY = 50;

                        await itemFrame.FadeTo(1, duration);
                        await itemFrame.TranslateTo(0, 0, duration, Easing.SpringOut);

                        await Task.Delay((int)delay);
                    }
                }
            }
        }

        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            // Navigate to the checkout page
            // Use "///checkout" if you need an absolute route
            await Shell.Current.GoToAsync("checkout");
        }

        private void OnIncreaseQuantity(object sender, EventArgs e)
        {
            // Increase the quantity of the selected item
            if (sender is Button button && button.Parent is StackLayout stackLayout)
            {
                // Find the Label in this stack that shows quantity
                var quantityLabel = stackLayout.Children.FirstOrDefault(c => c is Label) as Label;
                if (quantityLabel != null && int.TryParse(quantityLabel.Text, out int quantity))
                {
                    quantity++;
                    quantityLabel.Text = quantity.ToString();

                    // Update the total price
                    UpdateTotalPrice();
                }
            }
        }

        private void OnDecreaseQuantity(object sender, EventArgs e)
        {
            // Decrease the quantity of the selected item
            if (sender is Button button && button.Parent is StackLayout stackLayout)
            {
                var quantityLabel = stackLayout.Children.FirstOrDefault(c => c is Label) as Label;
                if (quantityLabel != null && int.TryParse(quantityLabel.Text, out int quantity))
                {
                    if (quantity > 1)
                    {
                        quantity--;
                        quantityLabel.Text = quantity.ToString();

                        // Update the total price
                        UpdateTotalPrice();
                    }
                    else
                    {
                        // Remove the item from the cart
                        RemoveCartItem(button);
                    }
                }
            }
        }

        private void RemoveCartItem(Button button)
        {
            // Find the parent Frame and remove it from "CartItemsLayout"
            View parent = button;
            while (parent != null && !(parent is Frame))
            {
                parent = parent.Parent as View;
            }

            if (parent != null && parent.Parent is StackLayout cartItemsLayout)
            {
                cartItemsLayout.Children.Remove(parent);

                // Update total price and cart count
                UpdateTotalPrice();
                UpdateCartCount();
            }
        }

        private void UpdateTotalPrice()
        {
            // Calculate new total price (simplified)
            decimal subtotal = 0;

            var stackLayout = this.FindByName<StackLayout>("CartItemsLayout");
            if (stackLayout != null)
            {
                foreach (var item in stackLayout.Children)
                {
                    if (item is Frame itemFrame)
                    {
                        // Suppose itemFrame has a Grid inside with "PriceLabel" and "QuantityLabel"
                        if (itemFrame.Content is Grid grid)
                        {
                            var priceLabel = grid.FindByName<Label>("PriceLabel");
                            var quantityLabel = grid.FindByName<Label>("QuantityLabel");

                            if (priceLabel != null && quantityLabel != null)
                            {
                                string priceText = priceLabel.Text.Replace("$", "");
                                if (decimal.TryParse(priceText, out decimal price) &&
                                    int.TryParse(quantityLabel.Text, out int quantity))
                                {
                                    subtotal += price * quantity;
                                }
                            }
                        }
                    }
                }
            }

            // Update the subtotal, tax, and total labels if they exist
            var subtotalLabel = this.FindByName<Label>("SubtotalLabel");
            var taxLabel = this.FindByName<Label>("TaxLabel");
            var totalLabel = this.FindByName<Label>("TotalLabel");

            if (subtotalLabel != null && taxLabel != null && totalLabel != null)
            {
                subtotalLabel.Text = $"${subtotal:F2}";

                decimal tax = subtotal * 0.08m; // 8% tax rate
                taxLabel.Text = $"${tax:F2}";

                decimal deliveryFee = 2.99m;
                decimal total = subtotal + tax + deliveryFee;
                totalLabel.Text = $"${total:F2}";
            }
        }

        private void UpdateCartCount()
        {
            // Update the cart count if you have a label named "CartCountLabel"
            var stackLayout = this.FindByName<StackLayout>("CartItemsLayout");
            if (stackLayout != null)
            {
                int count = stackLayout.Children.Count;
                var countLabel = this.FindByName<Label>("CartCountLabel");
                if (countLabel != null)
                {
                    countLabel.Text = $"{count} items in your cart";
                }
            }
        }
    }
}
