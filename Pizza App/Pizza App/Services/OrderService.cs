using System.Collections.Generic;
using System.Threading.Tasks;
using Pizza_App.Models;
using SQLite;

namespace Pizza_App.Services
{
    // Service to manage CRUD operations for Order objects using SQLite.
    public class OrderService
    {
        // Reference to the SQLite database connection.
        private readonly SQLiteAsyncConnection _database;

        public OrderService()
        {
            _database = SQLiteService.Database;
        }

        // Retrieves all orders.
        public Task<List<Order>> GetOrdersAsync()
        {
            return _database.Table<Order>().ToListAsync();
        }

        // Retrieves a specific order by its ID.
        public Task<Order> GetOrderAsync(int orderId)
        {
            return _database.Table<Order>().Where(o => o.OrderId == orderId).FirstOrDefaultAsync();
        }

        // Adds a new order.
        public Task<int> AddOrderAsync(Order order)
        {
            return _database.InsertAsync(order);
        }

        // Updates an existing order.
        public Task<int> UpdateOrderAsync(Order order)
        {
            return _database.UpdateAsync(order);
        }

        // Deletes an order.
        public Task<int> DeleteOrderAsync(Order order)
        {
            return _database.DeleteAsync(order);
        }
    }
}
