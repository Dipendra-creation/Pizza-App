using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.ViewModels;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            // Assign the ProfileViewModel to the BindingContext to handle all business logic.
            BindingContext = new ProfileViewModel();
        }
    }
}
