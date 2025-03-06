using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pizza_App.Services;
using Pizza_App.Views;

namespace Pizza_App
{
    public partial class App : Application
    {

        public App ()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}
