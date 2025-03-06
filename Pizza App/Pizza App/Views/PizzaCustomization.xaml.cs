using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Allows receiving "pizzaId" via Shell navigation, e.g., GoToAsync("customization?pizzaId=1")
    [QueryProperty("PizzaId", "pizzaId")]
    public partial class PizzaCustomization : ContentPage
    {
        private string _pizzaId;
        private decimal _basePrice = 9.99m;
        private decimal _currentPrice = 9.99m;

        public string PizzaId
        {
            get => _pizzaId;
            set
            {
                _pizzaId = value;
                LoadPizza(value);
            }
        }

        public PizzaCustomization()
        {
            InitializeComponent();
        }

        private void LoadPizza(string id)
        {
            // In a real app, load the pizza details from a service or database
            // For now, just update the UI with default values
            UpdatePrice();
        }

        private void UpdatePrice()
        {
            // Find the Label named "PriceLabel" in XAML
            var priceLabel = this.FindByName<Label>("PriceLabel");
            if (priceLabel != null)
            {
                priceLabel.Text = $"${_currentPrice:F2}";
            }
        }

        // ─────────────────────────────────────────────────────────────
        // SIZE SELECTION
        // ─────────────────────────────────────────────────────────────
        private void OnSizeSelected(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null)
            {
                // Reset all size options
                foreach (var child in SizeContainer.Children)
                {
                    if (child is Frame frame)
                    {
                        // Revert background to unselected color
                        frame.BackgroundColor = Application.Current.Resources.TryGetValue(
                            Application.Current.UserAppTheme == OSAppTheme.Dark
                                ? "SurfaceColorDark"
                                : "SurfaceColor",
                            out var unselectedColor)
                                ? (Color)unselectedColor
                                : Color.White;
                    }
                }

                // Highlight the selected option
                if (view.Parent is Frame selectedFrame)
                {
                    selectedFrame.BackgroundColor = Application.Current.Resources.TryGetValue(
                        Application.Current.UserAppTheme == OSAppTheme.Dark
                            ? "PrimaryColorDark"
                            : "PrimaryColor",
                        out var selectedColor)
                            ? (Color)selectedColor
                            : Color.Red;

                    // Update price based on ClassId (small, medium, large)
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

                    UpdatePrice();
                }
            }
        }

        // ─────────────────────────────────────────────────────────────
        // CRUST SELECTION
        // ─────────────────────────────────────────────────────────────
        private void OnCrustSelected(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null)
            {
                // Reset all crust options
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

                // Highlight the selected crust
                if (view.Parent is Frame selectedFrame)
                {
                    selectedFrame.BackgroundColor = Application.Current.Resources.TryGetValue(
                        Application.Current.UserAppTheme == OSAppTheme.Dark
                            ? "PrimaryColorDark"
                            : "PrimaryColor",
                        out var selectedColor)
                            ? (Color)selectedColor
                            : Color.Red;

                    // If it's "thick" crust, add $1
                    if (view.ClassId == "thick")
                    {
                        _currentPrice += 1.00m;
                    }
                    else
                    {
                        // If user switches back to thin, you'd subtract the $1 
                        // if they previously had thick selected. 
                        // This is a simplified approach.
                    }

                    UpdatePrice();
                }
            }
        }

        // ─────────────────────────────────────────────────────────────
        // TOPPING SELECTION
        // ─────────────────────────────────────────────────────────────
        private void OnToppingSelected(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null && view.Parent is Frame toppingFrame)
            {
                // Check the "selected" color
                var selectedColor = Application.Current.Resources.TryGetValue(
                    Application.Current.UserAppTheme == OSAppTheme.Dark
                        ? "PrimaryColorDark"
                        : "PrimaryColor",
                    out var sc)
                        ? (Color)sc
                        : Color.Red;

                // Check the "unselected" color
                var unselectedColor = Application.Current.Resources.TryGetValue(
                    Application.Current.UserAppTheme == OSAppTheme.Dark
                        ? "SurfaceColorDark"
                        : "SurfaceColor",
                    out var uc)
                        ? (Color)uc
                        : Color.White;

                // Toggle selection
                if (toppingFrame.BackgroundColor == selectedColor)
                {
                    // Deselect
                    toppingFrame.BackgroundColor = unselectedColor;
                    _currentPrice -= 0.75m;
                }
                else
                {
                    // Select
                    toppingFrame.BackgroundColor = selectedColor;
                    _currentPrice += 0.75m;
                }

                UpdatePrice();
            }
        }

        // ─────────────────────────────────────────────────────────────
        // ADD TO CART
        // ─────────────────────────────────────────────────────────────
        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            // In a real app, you'd add the pizza to a cart or call a service
            await DisplayAlert("Success", "Pizza added to cart!", "OK");

            // Navigate back (or to "cart") after adding
            await Shell.Current.GoToAsync("..");
        }
    }
}
