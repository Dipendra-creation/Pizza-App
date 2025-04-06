using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Services;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Login page.
    // Implements properties for user input and commands to execute login and navigate to the signup page.
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

        // Commands bound to UI elements for login and signup navigation.
        public ICommand LoginCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }

        private readonly AuthenticationService authService;

        public LoginViewModel()
        {
            // Initialize the authentication service.
            authService = new AuthenticationService();

            // Define the commands using async lambdas.
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            NavigateToSignUpCommand = new Command(async () => await ExecuteNavigateToSignUpCommand());
        }

        // Executes the login process.
        private async Task ExecuteLoginCommand()
        {
            // Validate that both username and password are provided.
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter both username and password.", "OK");
                return;
            }

            // Attempt to authenticate the user.
            var user = await authService.LoginAsync(Username, Password);
            if (user != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Login successful.", "OK");
                // Navigate to the Home page using an absolute route.
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
