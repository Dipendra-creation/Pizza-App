using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.ViewModels;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        public Cart()
        {
            InitializeComponent();
            // Set the BindingContext to the CartViewModel to enable data binding.
            BindingContext = new CartViewModel();
        }

        // Optionally, if you want to trigger visual animations when the page appears,
        // you can override OnAppearing here. Any animation logic should ideally be 
        // handled by the ViewModel or via behaviors.
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // For example, you could trigger a LoadCartCommand here if needed:
            // await (BindingContext as CartViewModel)?.LoadCartCommand.ExecuteAsync(null);
        }
    }
}
