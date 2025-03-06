using System;
using System.Collections.Generic;
using Pizza_App.Views;
using Xamarin.Forms;

namespace Pizza_App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Register routes for navigation
            Routing.RegisterRoute(nameof(PizzaCustomization), typeof(PizzaCustomization));
            Routing.RegisterRoute(nameof(Checkout), typeof(Checkout));
        }

    }
}
