using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.ViewModels;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {
        // Constructor: Initializes the Signup page and sets the BindingContext to the SignupViewModel.
        public Signup()
        {
            InitializeComponent();
            BindingContext = new SignupViewModel();
        }
    }
}
