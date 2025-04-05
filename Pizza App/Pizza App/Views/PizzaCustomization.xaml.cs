using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            UpdatePrice();
        }

        private void UpdatePrice()
        {
            var priceLabel = this.FindByName<Label>("PriceLabel");
            if (priceLabel != null)
            {
                priceLabel.Text = $"${_currentPrice:F2}";
            }
        }

        private void OnSizeSelected(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null)
            {
                foreach (var child in SizeContainer.Children)
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

                if (view.Parent is Frame selectedFrame)
                {
                    selectedFrame.BackgroundColor = Application.Current.Resources.TryGetValue(
                        Application.Current.UserAppTheme == OSAppTheme.Dark
                            ? "PrimaryColorDark"
                            : "PrimaryColor",
                        out var selectedColor)
                        ? (Color)selectedColor
                        : Color.Red;

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

        private void OnCrustSelected(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null)
            {
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

                if (view.Parent is Frame selectedFrame)
                {
                    selectedFrame.BackgroundColor = Application.Current.Resources.TryGetValue(
                        Application.Current.UserAppTheme == OSAppTheme.Dark
                            ? "PrimaryColorDark"
                            : "PrimaryColor",
                        out var selectedColor)
                        ? (Color)selectedColor
                        : Color.Red;

                    if (view.ClassId == "thick")
                    {
                        _currentPrice += 1.00m;
                    }

                    UpdatePrice();
                }
            }
        }

        private async void OnToppingSelected(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {
                var primaryColor = (Color)Application.Current.Resources["PrimaryColor"];
                var surfaceColor = (Color)Application.Current.Resources["SurfaceColor"];

                var selectedToppings = ToppingsContainer.Children.OfType<Frame>()
                    .Where(f => f.BackgroundColor == primaryColor).ToList();

                bool isAlreadySelected = frame.BackgroundColor == primaryColor;

                if (!isAlreadySelected && selectedToppings.Count >= 5)
                {
                    await frame.TranslateTo(-10, 0, 50);
                    await frame.TranslateTo(10, 0, 50);
                    await frame.TranslateTo(0, 0, 50);
                    return;
                }

                frame.BackgroundColor = isAlreadySelected ? surfaceColor : primaryColor;
                _currentPrice += isAlreadySelected ? -0.75m : 0.75m;

                UpdatePrice();

                int count = ToppingsContainer.Children.OfType<Frame>()
                    .Count(f => f.BackgroundColor == primaryColor);
                ToppingCounterLabel.Text = $"{count}/5 selected";
            }
        }

        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "Pizza added to cart!", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}
