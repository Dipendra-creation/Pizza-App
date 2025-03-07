using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    // Enable XAML compilation for better performance and error detection.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        // Constructor: Initializes the Cart page.
        public Cart()
        {
            InitializeComponent();
        }

        // Override OnAppearing to perform animations when the page appears.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Animate cart items (Frames) when the page appears.
            AnimateItems();
        }

        /// <summary>
        /// Animates each cart item in the CartItemsLayout StackLayout.
        /// Each Frame (cart item) fades in and translates upward with a slight delay between items.
        /// </summary>
        private async void AnimateItems()
        {
            // Find the StackLayout named "CartItemsLayout" defined in XAML.
            var stackLayout = this.FindByName<StackLayout>("CartItemsLayout");
            if (stackLayout != null)
            {
                // Animation timing parameters.
                uint duration = 300; // Duration for fade and translation (in milliseconds).
                uint delay = 100;    // Delay between animations for each item.

                // Iterate over each child in the StackLayout.
                foreach (var item in stackLayout.Children)
                {
                    // Only animate items that are Frames.
                    if (item is Frame itemFrame)
                    {
                        // Initialize animation properties.
                        itemFrame.Opacity = 0;
                        itemFrame.TranslationY = 50;

                        // Animate fade in.
                        await itemFrame.FadeTo(1, duration);
                        // Animate translation to original position using a spring easing function.
                        await itemFrame.TranslateTo(0, 0, duration, Easing.SpringOut);

                        // Delay before starting the animation for the next item.
                        await Task.Delay((int)delay);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Checkout button click event.
        /// Navigates the user to the checkout page.
        /// </summary>
        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            // Navigate to the checkout page using an absolute route.
            await Shell.Current.GoToAsync("///checkout");
        }

        /// <summary>
        /// Increases the quantity of a cart item.
        /// Updates the corresponding quantity label and recalculates the total price.
        /// </summary>
        private void OnIncreaseQuantity(object sender, EventArgs e)
        {
            // Ensure the sender is a Button and its parent is a StackLayout.
            if (sender is Button button && button.Parent is StackLayout stackLayout)
            {
                // Find the first Label in the StackLayout (assumed to be the quantity label).
                var quantityLabel = stackLayout.Children.FirstOrDefault(c => c is Label) as Label;
                if (quantityLabel != null && int.TryParse(quantityLabel.Text, out int quantity))
                {
                    // Increase the quantity and update the label.
                    quantity++;
                    quantityLabel.Text = quantity.ToString();
                    // Recalculate the total price.
                    UpdateTotalPrice();
                }
            }
        }

        /// <summary>
        /// Decreases the quantity of a cart item.
        /// If quantity falls below 1, removes the item from the cart.
        /// Otherwise, updates the quantity label and recalculates the total price.
        /// </summary>
        private void OnDecreaseQuantity(object sender, EventArgs e)
        {
            // Ensure the sender is a Button and its parent is a StackLayout.
            if (sender is Button button && button.Parent is StackLayout stackLayout)
            {
                // Find the quantity label within the StackLayout.
                var quantityLabel = stackLayout.Children.FirstOrDefault(c => c is Label) as Label;
                if (quantityLabel != null && int.TryParse(quantityLabel.Text, out int quantity))
                {
                    if (quantity > 1)
                    {
                        // Decrease the quantity if greater than 1.
                        quantity--;
                        quantityLabel.Text = quantity.ToString();
                        UpdateTotalPrice();
                    }
                    else
                    {
                        // Remove the cart item if quantity is 1.
                        RemoveCartItem(button);
                    }
                }
            }
        }

        /// <summary>
        /// Removes a cart item from the cart.
        /// Finds the parent Frame of the button and removes it from the CartItemsLayout.
        /// Updates the total price and cart count after removal.
        /// </summary>
        private void RemoveCartItem(Button button)
        {
            // Traverse up the visual tree to find the Frame that contains the cart item.
            View parent = button;
            while (parent != null && !(parent is Frame))
            {
                parent = parent.Parent as View;
            }

            // Once found, remove the Frame from its parent StackLayout.
            if (parent != null && parent.Parent is StackLayout cartItemsLayout)
            {
                cartItemsLayout.Children.Remove(parent);
                // Recalculate the total price.
                UpdateTotalPrice();
                // Update the cart count display.
                UpdateCartCount();
            }
        }

        /// <summary>
        /// Updates the total price of the items in the cart.
        /// Iterates through the CartItemsLayout, parses price and quantity, and calculates the subtotal.
        /// </summary>
        private void UpdateTotalPrice()
        {
            decimal subtotal = 0;
            var stackLayout = this.FindByName<StackLayout>("CartItemsLayout");
            if (stackLayout != null)
            {
                // Iterate through each cart item (Frame) in the StackLayout.
                foreach (var item in stackLayout.Children)
                {
                    if (item is Frame itemFrame && itemFrame.Content is Grid grid)
                    {
                        // Retrieve the price label (assumes label text starts with "$").
                        var priceLbl = grid.Children.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("$"));
                        // Retrieve the quantity label (assumes label text begins with a digit).
                        var quantityLbl = grid.Children.OfType<StackLayout>()
                            .SelectMany(sl => sl.Children.OfType<Label>())
                            .FirstOrDefault(l => !string.IsNullOrWhiteSpace(l.Text) && char.IsDigit(l.Text[0]));

                        if (priceLbl != null && quantityLbl != null)
                        {
                            // Remove the dollar sign and parse the price.
                            string priceText = priceLbl.Text.Replace("$", "");
                            if (decimal.TryParse(priceText, out decimal price) &&
                                int.TryParse(quantityLbl.Text, out int quantity))
                            {
                                // Accumulate the subtotal.
                                subtotal += price * quantity;
                            }
                        }
                    }
                }
            }

            // Update any labels or UI elements with the new subtotal here.
            // For this example, the subtotal is simply written to the console.
            Console.WriteLine($"New subtotal: {subtotal}");
        }

        /// <summary>
        /// Updates the cart item count display.
        /// Counts the number of items in the CartItemsLayout and updates the CartCountLabel.
        /// </summary>
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
