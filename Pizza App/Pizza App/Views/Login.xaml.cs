using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.ViewModels;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        // Constructor: Initializes the Login page and sets up data binding.
        public Login()
        {
            InitializeComponent();
            // Assign the LoginViewModel to the BindingContext so that the UI binds to its properties and commands.
            BindingContext = new LoginViewModel();
        }
    }
}
