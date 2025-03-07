using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    // Enable XAML compilation for improved performance and compile-time error checking.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // This attribute enables receiving the "pizzaId" query parameter via Shell navigation,
    // allowing calls such as GoToAsync("customization?pizzaId=1") to pass data into the page.
    [QueryProperty("PizzaId", "pizzaId")]
    public partial class PizzaCustomization : ContentPage
    {
        // Private backing field for the PizzaId property.
        private string _pizzaId;
        // Base price of a pizza without any additions.
        private decimal _basePrice = 9.99m;
        // Current price which is updated based on selected options.
        private decimal _currentPrice = 9.99m;

        // Public property that receives the "pizzaId" from navigation.
        // When set, it calls LoadPizza to update the UI accordingly.
        public string PizzaId
        {
            get => _pizzaId;
            set
            {
                _pizzaId = value;
                LoadPizza(value);
            }
        }

        // Constructor: Initializes the page components.
        public PizzaCustomization()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads pizza details based on the given id.
        /// In a real application, this method would retrieve pizza details from a service or database.
        /// Here, it simply calls UpdatePrice to refresh the displayed price.
        /// </summary>
        /// <param name="id">The pizza identifier.</param>
        private void LoadPizza(string id)
        {
            // TODO: Load pizza details from a data source based on the provided id.
            UpdatePrice();
        }

        /// <summary>
        /// Updates the displayed price using the current price value.
        /// Finds the PriceLabel defined in XAML and updates its text.
        /// </summary>
        private void UpdatePrice()
        {
            var priceLabel = this.FindByName<Label>("PriceLabel");
            if (priceLabel != null)
            {
                priceLabel.Text = $"${_currentPrice:F2}";
            }
        }

        // ─────────────────────────────────────────────────────────────
        // SIZE SELECTION HANDLER
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles the selection of a pizza size.
        /// Resets all size options to the unselected state, highlights the tapped option,
        /// and updates the current price based on the selected size.
        /// </summary>
        private void OnSizeSelected(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null)
            {
                // Reset the background color of all size option frames.
                foreach (var child in SizeContainer.Children)
                {
                    if (child is Frame frame)
                    {
                        // Get the unselected color based on the current app theme.
                        frame.BackgroundColor = Application.Current.Resources.TryGetValue(
                            Application.Current.UserAppTheme == OSAppTheme.Dark
                                ? "SurfaceColorDark"
                                : "SurfaceColor",
                            out var unselectedColor)
                                ? (Color)unselectedColor
                                : Color.White;
                    }
                }

                // Highlight the selected size option.
                if (view.Parent is Frame selectedFrame)
                {
                    // Get the selected color based on the current app theme.
                    selectedFrame.BackgroundColor = Application.Current.Resources.TryGetValue(
                        Application.Current.UserAppTheme == OSAppTheme.Dark
                            ? "PrimaryColorDark"
                            : "PrimaryColor",
                        out var selectedColor)
                            ? (Color)selectedColor
                            : Color.Red;

                    // Update the price based on the selected size option.
                    // The ClassId is used to identify the size ("small", "medium", or "large").
                    switch (view.ClassId)
                    {
                        case "small":
                            _currentPrice = _basePrice;
                            break;
                        case "medium":
                            _currentPrice = _basePrice + 2.00m;
                            break;
                        case "large":
                            _currentPrice = _basePrice + 4.00m;
                            break;
                    }

                    // Refresh the displayed price.
                    UpdatePrice();
                }
            }
        }

        // ─────────────────────────────────────────────────────────────
        // CRUST SELECTION HANDLER
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles the selection of a pizza crust.
        /// Resets all crust options and highlights the selected crust.
        /// If "thick" crust is selected, updates the price by adding an extra cost.
        /// </summary>
        private void OnCrustSelected(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null)
            {
                // Reset the background color for all crust option frames.
                foreach (var child in CrustContainer.Children)
                {
                    if (child is Frame frame)
                    {
                        frame.BackgroundColor = Application.Current.Resources.TryGetValue(
                            Application.Current.UserAppTheme == OSAppTheme.Dark
                                ? "SurfaceColorDark"
                                : "SurfaceColor",
                            out var unselectedColor)
                                ? (Color)unselectedColor
                                : Color.White;
                    }
                }

                // Highlight the selected crust option.
                if (view.Parent is Frame selectedFrame)
                {
                    selectedFrame.BackgroundColor = Application.Current.Resources.TryGetValue(
                        Application.Current.UserAppTheme == OSAppTheme.Dark
                            ? "PrimaryColorDark"
                            : "PrimaryColor",
                        out var selectedColor)
                            ? (Color)selectedColor
                            : Color.Red;

                    // Update the price if the "thick" crust is selected.
                    // A more comprehensive solution would handle toggling between crust options.
                    if (view.ClassId == "thick")
                    {
                        _currentPrice += 1.00m;
                    }
                    else
                    {
                        // If switching back to thin, consider subtracting the extra cost.
                        // This simplified approach does not implement full toggling logic.
                    }

                    UpdatePrice();
                }
            }
        }

        // ─────────────────────────────────────────────────────────────
        // TOPPING SELECTION HANDLER
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles topping selection toggles.
        /// Toggles the background color of the topping frame and updates the current price accordingly.
        /// Each topping adds or subtracts a fixed amount from the price.
        /// </summary>
        private void OnToppingSelected(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null && view.Parent is Frame toppingFrame)
            {
                // Retrieve the selected and unselected colors from the application resources.
                var selectedColor = Application.Current.Resources.TryGetValue(
                    Application.Current.UserAppTheme == OSAppTheme.Dark
                        ? "PrimaryColorDark"
                        : "PrimaryColor",
                    out var sc)
                        ? (Color)sc
                        : Color.Red;

                var unselectedColor = Application.Current.Resources.TryGetValue(
                    Application.Current.UserAppTheme == OSAppTheme.Dark
                        ? "SurfaceColorDark"
                        : "SurfaceColor",
                    out var uc)
                        ? (Color)uc
                        : Color.White;

                // Toggle the selection state of the topping.
                if (toppingFrame.BackgroundColor == selectedColor)
                {
                    // Deselect the topping: revert to unselected color and subtract its cost.
                    toppingFrame.BackgroundColor = unselectedColor;
                    _currentPrice -= 0.75m;
                }
                else
                {
                    // Select the topping: apply the selected color and add its cost.
                    toppingFrame.BackgroundColor = selectedColor;
                    _currentPrice += 0.75m;
                }

                // Refresh the displayed price.
                UpdatePrice();
            }
        }

        // ─────────────────────────────────────────────────────────────
        // ADD TO CART HANDLER
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles the "Add to Cart" button click event.
        /// In a real app, this method would add the customized pizza to the cart.
        /// For now, it displays a success message and navigates back.
        /// </summary>
        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            // Display a confirmation alert indicating success.
            await DisplayAlert("Success", "Pizza added to cart!", "OK");

            // Navigate back to the previous page (or to the cart page) using Shell navigation.
            await Shell.Current.GoToAsync("..");
        }
    }
}
