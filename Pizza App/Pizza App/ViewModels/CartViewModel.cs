using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Pizza_App.Models;
using Pizza_App.Services;

namespace Pizza_App.ViewModels
{
    // ViewModel for the Cart page.
    // Manages the list of CartItems and provides commands to load, update, and remove items.
    public class CartViewModel : BaseViewModel
    {
        private readonly CartService cartService;

        // Observable collection bound to the Cart page UI.
        public ObservableCollection<CartItem> CartItems { get; } = new ObservableCollection<CartItem>();

        // Command to load cart items from the database.
        public ICommand LoadCartCommand { get; }
        // Command to remove a specific item from the cart.
        public ICommand RemoveItemCommand { get; }
        // Command to increase the quantity of a cart item.
        public ICommand IncreaseQuantityCommand { get; }
        // Command to decrease the quantity of a cart item.
        public ICommand DecreaseQuantityCommand { get; }

        public CartViewModel()
        {
            cartService = new CartService();
            LoadCartCommand = new Command(async () => await ExecuteLoadCartItemsCommand());
            RemoveItemCommand = new Command<CartItem>(async (item) => await ExecuteRemoveItemCommand(item));
            IncreaseQuantityCommand = new Command<CartItem>(async (item) => await ExecuteIncreaseQuantityCommand(item));
            DecreaseQuantityCommand = new Command<CartItem>(async (item) => await ExecuteDecreaseQuantityCommand(item));
        }

        // Loads all cart items from the database and updates the observable collection.
        private async Task ExecuteLoadCartItemsCommand()
        {
            CartItems.Clear();
            var items = await cartService.GetCartItemsAsync();
            foreach (var item in items)
            {
                CartItems.Add(item);
            }
        }

        // Removes a specific cart item.
        private async Task ExecuteRemoveItemCommand(CartItem item)
        {
            if (item != null)
            {
                await cartService.DeleteCartItemAsync(item);
                CartItems.Remove(item);
            }
        }

        // Increases the quantity of a cart item and updates its total price.
        private async Task ExecuteIncreaseQuantityCommand(CartItem item)
        {
            if (item != null)
            {
                item.Quantity++;
                item.TotalPrice = item.Quantity * item.UnitPrice;
                await cartService.UpdateCartItemAsync(item);
                // Optionally, raise property changed on CartItems if needed.
            }
        }

        // Decreases the quantity of a cart item. If quantity reaches 0, the item is removed.
        private async Task ExecuteDecreaseQuantityCommand(CartItem item)
        {
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    item.TotalPrice = item.Quantity * item.UnitPrice;
                    await cartService.UpdateCartItemAsync(item);
                }
                else
                {
                    // Remove the item if quantity is 1.
                    await ExecuteRemoveItemCommand(item);
                }
            }
        }
    }
}
