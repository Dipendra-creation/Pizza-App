using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Models;
using Pizza_App.Services;
using Pizza_App.Models.Pizza_App.Models;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Profile page.
    // Provides properties for the user's profile information, a command to update the profile,
    // and a command to log out.
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

            // For demonstration purposes, initialize with dummy data.
            // In a production app, load the current user's details from persistent storage.
            User = new User
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1 (555) 123-4567",
                Address = "123 Main St"
            };

            UpdateProfileCommand = new Command(async () => await ExecuteUpdateProfileCommand());
            LogoutCommand = new Command(async () => await ExecuteLogoutCommand());
        }

        // Executes the profile update process by updating the user data in the database.
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

        // Executes the logout process by navigating back to the Login page.
        private async Task ExecuteLogoutCommand()
        {
            // Optional: Clear any session data here if necessary.
            await Shell.Current.GoToAsync("///login");
        }
    }
}
