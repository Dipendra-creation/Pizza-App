using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Models;
using Pizza_App.Services;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Order History page.
    // Loads the list of orders from the database and exposes them via an ObservableCollection.
    public class OrderHistoryViewModel : BaseViewModel
    {
        private readonly OrderService orderService;
        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        // Command to refresh the list of orders.
        public ICommand RefreshOrdersCommand { get; }

        public OrderHistoryViewModel()
        {
            orderService = new OrderService();
            RefreshOrdersCommand = new Command(async () => await LoadOrdersAsync());
            // Load orders when the ViewModel is initialized.
            Task.Run(async () => await LoadOrdersAsync());
        }

        // Asynchronously loads orders from the database.
        private async Task LoadOrdersAsync()
        {
            Orders.Clear();
            var orders = await orderService.GetOrdersAsync();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}
