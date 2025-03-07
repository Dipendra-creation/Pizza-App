using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        // Called when the Dark Mode switch is toggled
        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            // Example: Toggle dark mode in your app
            // If you have a static method like App.ToggleDarkMode(), call it here.
            // For demonstration:
            bool isDarkMode = e.Value;
            DisplayAlert("Dark Mode",
                $"Dark mode toggled: {(isDarkMode ? "ON" : "OFF")}",
                "OK");

            // If you actually implement dark mode, do it here:
            // App.ToggleDarkMode(isDarkMode);
        }

        // Called when user clicks "Edit Profile" button
        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            // For now, just show a placeholder alert
            await DisplayAlert("Edit Profile",
                "Editing profile not implemented yet!",
                "OK");
        }

        // Called when user clicks "Add Payment Method" button
        private async void OnAddPaymentMethodClicked(object sender, EventArgs e)
        {
            // Placeholder for adding payment method
            await DisplayAlert("Payment Method",
                "Add payment method not implemented yet!",
                "OK");
        }

        // Called when user clicks "Logout" button
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout",
                "Are you sure you want to logout?",
                "Yes", "No");

            if (confirm)
            {
                // Perform actual logout logic here (e.g., clear user session, navigate to login)
                // For now, just show an alert
                await DisplayAlert("Logged Out",
                    "You have been logged out.",
                    "OK");

                // Example: navigate to a login page or root
                // await Shell.Current.GoToAsync("//login");
            }
        }
    }
}
