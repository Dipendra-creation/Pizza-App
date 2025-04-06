using Pizza_App.Models;
using Pizza_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaCustomization : ContentPage
    {
        public PizzaCustomization(Pizza selectedPizza)
        {
            InitializeComponent();
            BindingContext = new PizzaCustomizationViewModel(selectedPizza);
        }
    }
}
