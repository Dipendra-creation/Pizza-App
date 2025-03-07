using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pizza_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AnimateItems();
        }

        private async void AnimateItems()
        {
            // Attempt to find the CollectionView by name
            var pizzaCollectionView = this.FindByName<CollectionView>("PizzaCollectionView");
            if (pizzaCollectionView == null || pizzaCollectionView.ItemsSource == null)
                return;

            uint duration = 300;
            uint delay = 100;

            // Convert the ItemsSource to an IEnumerable
            var items = pizzaCollectionView.ItemsSource as System.Collections.IEnumerable;
            if (items == null)
                return;

            // Animate each item (naive approach: 
            // creates new View instances from the DataTemplate instead of the actual rendered cells)
            foreach (var item in items)
            {
                var viewCell = pizzaCollectionView.ItemTemplate?.CreateContent() as View;
                if (viewCell != null)
                {
                    viewCell.Opacity = 0;
                    viewCell.TranslationY = 50;

                    await viewCell.FadeTo(1, duration);
                    await viewCell.TranslateTo(0, 0, duration, Easing.SpringOut);

                    await Task.Delay((int)delay);
                }
            }
        }

        private async void OnPizzaTapped(object sender, EventArgs e)
        {
            // Navigate to the pizza customization page
            // Pass a sample pizzaId (replace '1' as needed)
            await Shell.Current.GoToAsync("///customization?pizzaId=1");
        }
    }
}
