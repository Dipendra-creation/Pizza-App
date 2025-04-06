using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Models;
using Pizza_App.ViewModels;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }

        // Navigates to the customization page with the selected pizza.
        private async void OnPizzaTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is Pizza selectedPizza)
            {
                var route = $"customization?pizzaName={Uri.EscapeDataString(selectedPizza.Name)}";
                await Shell.Current.GoToAsync(route);
            }
        }

        // Optional: Animate items when the page appears.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Add animations if you want visual polish.
        }
    }
}
