using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Models;
using Pizza_App.ViewModels;
using Pizza_App.Services;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(PizzaId), "pizzaId")] // receives the ID from Shell navigation
    public partial class PizzaCustomization : ContentPage
    {
        private readonly PizzaService _pizzaService = new PizzaService();
        private PizzaCustomizationViewModel _viewModel;

        public PizzaCustomization()
        {
            InitializeComponent();

            // Set software/hardware back button behavior
            NavigationPage.SetHasBackButton(this, true);
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(async () => await Shell.Current.GoToAsync(".."))
            });
        }

        private string pizzaId;
        public string PizzaId
        {
            get => pizzaId;
            set
            {
                pizzaId = Uri.UnescapeDataString(value ?? string.Empty);
                if (int.TryParse(pizzaId, out int id))
                {
                    LoadPizzaAsync(id);
                }
                else
                {
                    // Invalid ID format
                    DisplayAlert("Error", "Invalid Pizza ID format.", "OK");
                    Shell.Current.GoToAsync("..");
                }
            }
        }

        private async void LoadPizzaAsync(int id)
        {
            try
            {
                var pizza = await _pizzaService.GetPizzaByIdAsync(id);
                if (pizza != null)
                {
                    _viewModel = new PizzaCustomizationViewModel(pizza);
                    BindingContext = _viewModel;
                }
                else
                {
                    await DisplayAlert("Error", $"Pizza with ID {id} not found.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to load pizza: {ex.Message}", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });
            return true;
        }
    }
}
