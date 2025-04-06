using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Models;
using Pizza_App.Services;
using Pizza_App.Models.Pizza_App.Models;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Signup page.
    // Exposes properties for user input and commands for signup operations.
    public class SignupViewModel : BaseViewModel
    {
        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private string email;
        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set
            {
                if (phone != value)
                {
                    phone = value;
                    OnPropertyChanged();
                }
            }
        }

        private string address;
        public string Address
        {
            get => address;
            set
            {
                if (address != value)
                {
                    address = value;
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

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        // Command to perform the signup operation.
        public ICommand SignupCommand { get; }
        // Command to navigate to the login page.
        public ICommand NavigateToLoginCommand { get; }

        private readonly AuthenticationService authService;

        public SignupViewModel()
        {
            authService = new AuthenticationService();
            SignupCommand = new Command(async () => await ExecuteSignupCommand());
            NavigateToLoginCommand = new Command(async () => await ExecuteNavigateToLoginCommand());
        }

        // Executes the signup process: validates inputs, creates a new user, and navigates to login on success.
        private async Task ExecuteSignupCommand()
        {
            // Validate that all fields are provided.
            if (string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Phone) ||
                string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }

            // Ensure the password and confirmation match.
            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Passwords do not match.", "OK");
                return;
            }

            // Create a new User instance with the provided data.
            var newUser = new User
            {
                FullName = FullName,
                Username = Username,
                Email = Email,
                PhoneNumber = Phone,
                Address = Address,
                Password = Password  // In production, hash and salt the password.
            };

            // Attempt to sign up the new user.
            bool result = await authService.SignUpAsync(newUser);
            if (result)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully.", "OK");
                // Navigate to the Login page upon successful signup.
                await Shell.Current.GoToAsync("///login");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Signup Failed", "An account with the same username or email already exists.", "OK");
            }
        }

        // Navigates the user to the Login page.
        private async Task ExecuteNavigateToLoginCommand()
        {
            await Shell.Current.GoToAsync("///login");
        }
    }
}
