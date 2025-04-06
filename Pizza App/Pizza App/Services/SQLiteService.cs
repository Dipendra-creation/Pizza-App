using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Pizza_App.Models;
using Pizza_App.Models.Pizza_App.Models;

namespace Pizza_App.Services
{
    public class SQLiteService
    {
        // Singleton instance of the SQLite connection.
        static SQLiteAsyncConnection database;

        // Property to access the SQLite connection.
        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (database == null)
                {
                    // Determine a path for the database file.
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PizzaApp.db3");
                    database = new SQLiteAsyncConnection(dbPath);
                }
                return database;
            }
        }

        // Initialize the database and create tables if they do not exist.
        public static async Task InitializeAsync()
        {
            // Create the necessary tables.
            await Database.CreateTableAsync<User>();
            await Database.CreateTableAsync<CartItem>();
            await Database.CreateTableAsync<Order>();
            await Database.CreateTableAsync<OrderItem>();
        }
    }
}
