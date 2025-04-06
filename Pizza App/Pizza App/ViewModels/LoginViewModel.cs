using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Services;
using Xamarin.Essentials;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Login page.
    // Exposes properties for user inputs and commands to perform login and navigate to the signup page.
    public class LoginViewModel : BaseViewModel
    {
        private string username;
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        // Command to trigger the login process.
        public ICommand LoginCommand { get; }
        // Command to navigate to the signup page.
        public ICommand NavigateToSignUpCommand { get; }

        private readonly AuthenticationService authService;

        public LoginViewModel()
        {
            authService = new AuthenticationService();
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            NavigateToSignUpCommand = new Command(async () => await ExecuteNavigateToSignUpCommand());
        }

        // Executes the login process.
        private async Task ExecuteLoginCommand()
        {
            // Validate input fields.
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter both username and password.", "OK");
                return;
            }

            // Attempt user authentication.
            var user = await authService.LoginAsync(Username, Password);
            if (user != null)
            {
                // Set the login flag to true.
                Preferences.Set("IsLoggedIn", true);
                await Application.Current.MainPage.DisplayAlert("Success", "Login successful.", "OK");
                // Navigate to the Home page (or AppShell) using an absolute route.
                await Shell.Current.GoToAsync("///home");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password.", "OK");
            }
        }

        // Navigates the user to the Signup page.
        private async Task ExecuteNavigateToSignUpCommand()
        {
            await Shell.Current.GoToAsync("///signup");
        }
    }
}
