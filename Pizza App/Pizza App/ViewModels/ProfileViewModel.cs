using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Models;
using Pizza_App.Services;
using Xamarin.Essentials;
using Pizza_App.Models.Pizza_App.Models;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Profile page.
    // Retrieves the logged-in user's profile from SQLite and provides commands to update the profile or log out.
    public class ProfileViewModel : BaseViewModel
    {
        private readonly UserService userService;

        private User user;
        public User User
        {
            get => user;
            set { user = value; OnPropertyChanged(); }
        }

        // Command to update the user profile.
        public ICommand UpdateProfileCommand { get; }
        // Command to log out.
        public ICommand LogoutCommand { get; }

        public ProfileViewModel()
        {
            userService = new UserService();

            UpdateProfileCommand = new Command(async () => await ExecuteUpdateProfileCommand());
            LogoutCommand = new Command(async () => await ExecuteLogoutCommand());

            // Load user details from persistent storage.
            LoadUserDetails();
        }

        // Loads the logged-in user's details from SQLite.
        private async void LoadUserDetails()
        {
            // Retrieve the username stored in Preferences after login.
            string username = Preferences.Get("LoggedInUsername", string.Empty);
            if (!string.IsNullOrEmpty(username))
            {
                User = await userService.GetUserAsync(username);
            }
            else
            {
                // If no user is found, you may choose to set default values or handle this scenario.
                User = new User
                {
                    FullName = "Unknown User",
                    Email = "unknown@example.com",
                    PhoneNumber = "",
                    Address = ""
                };
            }
        }

        // Executes the profile update by saving the updated user information to the database.
        private async Task ExecuteUpdateProfileCommand()
        {
            var result = await userService.UpdateUserAsync(User);
            if (result > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Profile updated successfully.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Profile update failed.", "OK");
            }
        }

        // Executes the logout process: clears the login flag and navigates back to the Login page.
        private async Task ExecuteLogoutCommand()
        {
            // Clear login-related preferences.
            Preferences.Set("IsLoggedIn", false);
            Preferences.Remove("LoggedInUsername");
            await Shell.Current.GoToAsync("///login");
        }
    }
}
