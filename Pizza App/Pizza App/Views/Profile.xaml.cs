using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    // Enable XAML compilation to improve performance and catch XAML-related errors at compile time.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        // Constructor: Initializes the Profile page and its components.
        public Profile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for when the Dark Mode switch is toggled.
        /// In a real application, you might call a method to update the app's theme.
        /// For now, it displays an alert showing whether dark mode is enabled.
        /// </summary>
        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            // Retrieve the current toggle value (true if dark mode is enabled).
            bool isDarkMode = e.Value;
            // Display an alert indicating the new state of dark mode.
            DisplayAlert("Dark Mode",
                $"Dark mode toggled: {(isDarkMode ? "ON" : "OFF")}",
                "OK");

            // If your app supports theme switching, you might call a method such as:
            // App.ToggleDarkMode(isDarkMode);
        }

        /// <summary>
        /// Event handler for when the "Edit Profile" button is clicked.
        /// Currently, it displays a placeholder alert.
        /// In a real application, it would navigate to or display a profile editing form.
        /// </summary>
        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Edit Profile",
                "Editing profile not implemented yet!",
                "OK");
        }

        /// <summary>
        /// Event handler for when the "Add Payment Method" button is clicked.
        /// Currently, it displays a placeholder alert.
        /// In a real application, it would launch a form or workflow for adding payment details.
        /// </summary>
        private async void OnAddPaymentMethodClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Payment Method",
                "Add payment method not implemented yet!",
                "OK");
        }

        /// <summary>
        /// Event handler for when the "Logout" button is clicked.
        /// Asks the user for confirmation before performing logout logic.
        /// Currently, it shows alerts and contains placeholder logic for logging out.
        /// </summary>
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Confirm the logout action with the user.
            bool confirm = await DisplayAlert("Logout",
                "Are you sure you want to logout?",
                "Yes", "No");

            if (confirm)
            {
                // Placeholder for actual logout logic such as clearing user session data.
                await DisplayAlert("Logged Out",
                    "You have been logged out.",
                    "OK");

                // Example: Navigate to a login page or the app's root.
                // await Shell.Current.GoToAsync("//login");
            }
        }
    }
}
