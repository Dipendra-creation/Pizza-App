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
            // Use "///checkout" if you need an absolute route from anywhere
            await Shell.Current.GoToAsync("///checkout");

        }

        private void OnIncreaseQuantity(object sender, EventArgs e)
        {
            if (sender is Button button && button.Parent is StackLayout stackLayout)
            {
                var quantityLabel = stackLayout.Children.FirstOrDefault(c => c is Label) as Label;
                if (quantityLabel != null && int.TryParse(quantityLabel.Text, out int quantity))
                {
                    quantity++;
                    quantityLabel.Text = quantity.ToString();
                    UpdateTotalPrice();
                }
            }
        }

        private void OnDecreaseQuantity(object sender, EventArgs e)
        {
            if (sender is Button button && button.Parent is StackLayout stackLayout)
            {
                var quantityLabel = stackLayout.Children.FirstOrDefault(c => c is Label) as Label;
                if (quantityLabel != null && int.TryParse(quantityLabel.Text, out int quantity))
                {
                    if (quantity > 1)
                    {
                        quantity--;
                        quantityLabel.Text = quantity.ToString();
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
            View parent = button;
            while (parent != null && !(parent is Frame))
            {
                parent = parent.Parent as View;
            }

            if (parent != null && parent.Parent is StackLayout cartItemsLayout)
            {
                cartItemsLayout.Children.Remove(parent);
                UpdateTotalPrice();
                UpdateCartCount();
            }
        }

        private void UpdateTotalPrice()
        {
            // Simplified price calculation
            decimal subtotal = 0;
            var stackLayout = this.FindByName<StackLayout>("CartItemsLayout");
            if (stackLayout != null)
            {
                foreach (var item in stackLayout.Children)
                {
                    if (item is Frame itemFrame && itemFrame.Content is Grid grid)
                    {
                        // If you named the price label "PriceLabel" and quantity label "QuantityLabel", you can do:
                        // var priceLabel = grid.FindByName<Label>("PriceLabel");
                        // var quantityLabel = grid.FindByName<Label>("QuantityLabel");
                        // For now, we parse from the existing text.

                        var priceLbl = grid.Children.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("$"));
                        var quantityLbl = grid.Children.OfType<StackLayout>()
                            .SelectMany(sl => sl.Children.OfType<Label>())
                            .FirstOrDefault(l => !string.IsNullOrWhiteSpace(l.Text) && char.IsDigit(l.Text[0]));

                        if (priceLbl != null && quantityLbl != null)
                        {
                            string priceText = priceLbl.Text.Replace("$", "");
                            if (decimal.TryParse(priceText, out decimal price) &&
                                int.TryParse(quantityLbl.Text, out int quantity))
                            {
                                subtotal += price * quantity;
                            }
                        }
                    }
                }
            }

            // If you have labels named SubtotalLabel, TaxLabel, TotalLabel, update them here:
            // e.g.,
            // var subtotalLabel = this.FindByName<Label>("SubtotalLabel");
            // if (subtotalLabel != null) { ... }

            // Just an example, no real label named here:
            Console.WriteLine($"New subtotal: {subtotal}");
        }

        private void UpdateCartCount()
        {
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
