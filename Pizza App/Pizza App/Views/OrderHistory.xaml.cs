using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.ViewModels;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderHistory : ContentPage
    {
        public OrderHistory()
        {
            InitializeComponent();
            // Set the BindingContext to an instance of OrderHistoryViewModel to enable MVVM binding.
            BindingContext = new OrderHistoryViewModel();
        }
    }
}
