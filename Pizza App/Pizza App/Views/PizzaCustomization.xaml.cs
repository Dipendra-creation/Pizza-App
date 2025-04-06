using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Models;
using Pizza_App.ViewModels;
using Pizza_App.Services;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("PizzaName", "pizzaName")] // receive pizzaName from navigation
    public partial class PizzaCustomization : ContentPage
    {
        private readonly PizzaService _pizzaService = new PizzaService();
        private PizzaCustomizationViewModel _viewModel;

        public PizzaCustomization()
        {
            InitializeComponent();

            // Enable back navigation with software and hardware
            NavigationPage.SetHasBackButton(this, true);
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(async () => await Shell.Current.GoToAsync(".."))
            });
        }

        private string pizzaName;
        public string PizzaName
        {
            get => pizzaName;
            set
            {
                pizzaName = Uri.UnescapeDataString(value ?? string.Empty);
                LoadPizzaAsync(pizzaName);
            }
        }

        private async void LoadPizzaAsync(string name)
        {
            var pizza = await _pizzaService.GetPizzaByNameAsync(name);
            if (pizza != null)
            {
                _viewModel = new PizzaCustomizationViewModel(pizza);
                BindingContext = _viewModel;
            }
            else
            {
                await DisplayAlert("Error", "Pizza not found", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }

        // Handle Android hardware back button
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