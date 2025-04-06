using System.Collections.Generic;
using System.Threading.Tasks;
using Pizza_App.Models;
using SQLite;

namespace Pizza_App.Services
{
    // Service to manage CRUD operations for CartItem objects using SQLite.
    public class CartService
    {
        // Reference to the SQLite database connection from SQLiteService.
        private readonly SQLiteAsyncConnection _database;

        public CartService()
        {
            _database = SQLiteService.Database;
        }

        // Retrieves all cart items.
        public Task<List<CartItem>> GetCartItemsAsync()
        {
            return _database.Table<CartItem>().ToListAsync();
        }

        // Adds a new cart item.
        public Task<int> AddCartItemAsync(CartItem item)
        {
            return _database.InsertAsync(item);
        }

        // Updates an existing cart item.
        public Task<int> UpdateCartItemAsync(CartItem item)
        {
            return _database.UpdateAsync(item);
        }

        // Deletes a cart item.
        public Task<int> DeleteCartItemAsync(CartItem item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
