using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Services;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        // Instance of the AuthenticationService to handle login operations.
        private readonly AuthenticationService authService = new AuthenticationService();

        public Login()
        {
            InitializeComponent();
        }

        // Handles the Login button click event.
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Retrieve user inputs.
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Validate that both fields are provided.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Validation Error", "Please enter both username and password.", "OK");
                return;
            }

            // Attempt user authentication.
            var user = await authService.LoginAsync(username, password);
            if (user != null)
            {
                await DisplayAlert("Success", "Login successful.", "OK");
                // Navigate to the Home page using absolute routing.
                await Shell.Current.GoToAsync("///home");
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid username or password.", "OK");
            }
        }

        // Handles the Sign-Up navigation event.
        private async void OnSignUpTapped(object sender, EventArgs e)
        {
            // Navigate to the Signup page.
            await Shell.Current.GoToAsync("///signup");
        }
    }
}
