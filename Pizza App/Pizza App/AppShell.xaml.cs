using System;
using Pizza_App.Views;
using Xamarin.Forms;

namespace Pizza_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes with string keys to match GoToAsync
            Routing.RegisterRoute("customization", typeof(PizzaCustomization));
            Routing.RegisterRoute("checkout", typeof(Checkout));
            Routing.RegisterRoute("signup", typeof(Signup));
        }
    }
}
