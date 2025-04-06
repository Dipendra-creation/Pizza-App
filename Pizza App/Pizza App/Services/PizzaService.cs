using System.Collections.Generic;
using System.Threading.Tasks;
using Pizza_App.Models;
using SQLite;

namespace Pizza_App.Services
{
    // Service for managing pizza data in the local SQLite database.
    public class PizzaService
    {
        private readonly SQLiteAsyncConnection _database;

        public PizzaService()
        {
            _database = SQLiteService.Database;
            // Ensure the Pizza table exists.
            _database.CreateTableAsync<Pizza>().Wait();
        }

        // Retrieves all pizzas from the database.
        public Task<List<Pizza>> GetPizzasAsync()
        {
            return _database.Table<Pizza>().ToListAsync();
        }

        // Adds a single pizza to the database.
        public Task<int> AddPizzaAsync(Pizza pizza)
        {
            return _database.InsertAsync(pizza);
        }

        // Adds multiple pizzas to the database.
        public Task<int> AddPizzasAsync(IEnumerable<Pizza> pizzas)
        {
            return _database.InsertAllAsync(pizzas);
        }
    }
}
