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
            // Register routes for navigation
            Routing.RegisterRoute(nameof(PizzaCustomization), typeof(PizzaCustomization));
            Routing.RegisterRoute(nameof(Checkout), typeof(Checkout));
            Routing.RegisterRoute(nameof(Signup), typeof(Signup));
        }
    }
}
