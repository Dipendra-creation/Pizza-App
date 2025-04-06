using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Models;
using Pizza_App.Services;
using Pizza_App.Models.Pizza_App.Models;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {
        // Instance of the authentication service for signup operations.
        private readonly AuthenticationService authService = new AuthenticationService();

        public Entry FullNameEntry { get; private set; }
        public Entry UsernameEntry { get; private set; }
        public Entry EmailEntry { get; private set; }
        public Entry PhoneEntry { get; private set; }
        public Entry AddressEntry { get; private set; }
        public Entry PasswordEntry { get; private set; }
        public Entry ConfirmPasswordEntry { get; private set; }

        public Signup()
        {
            InitializeComponent();
        }

        // Handles the Sign Up button click event.
        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            // Validate that all required fields are filled.
            if (string.IsNullOrWhiteSpace(FullNameEntry.Text) ||
                string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PhoneEntry.Text) ||
                string.IsNullOrWhiteSpace(AddressEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
            {
                await DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }

            // Ensure the password and confirmation match.
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                await DisplayAlert("Validation Error", "Passwords do not match.", "OK");
                return;
            }

            // Create a new user instance with the provided data.
            var newUser = new User
            {
                FullName = FullNameEntry.Text,
                Username = UsernameEntry.Text,
                Email = EmailEntry.Text,
                PhoneNumber = PhoneEntry.Text,
                Address = AddressEntry.Text,
                Password = PasswordEntry.Text // In production, remember to hash the password.
            };

            // Attempt to sign up the user using the AuthenticationService.
            bool result = await authService.SignUpAsync(newUser);
            if (result)
            {
                await DisplayAlert("Success", "Account created successfully.", "OK");
                // Navigate to the Login page after successful signup.
                await Shell.Current.GoToAsync("///login");
            }
            else
            {
                await DisplayAlert("Signup Failed", "An account with the same username or email already exists.", "OK");
            }
        }

        // Handles the tap gesture on the login label.
        private async void OnLoginTapped(object sender, EventArgs e)
        {
            // Navigate to the Login page.
            await Shell.Current.GoToAsync("///login");
        }
    }
}
