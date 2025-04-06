using System;
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

        // Seeds initial pizza data if none exist in the database.
        public async Task SeedDefaultPizzasAsync()
        {
            var existing = await GetPizzasAsync();
            if (existing.Count == 0)
            {
                var pizzas = new List<Pizza>
                {
                    new Pizza { Id = 1, Name = "Margherita", Price = 10.99m, Description = "Classic pizza with tomato, mozzarella, and basil", ImageName = "pizza_margherita.jpg" },
                    new Pizza { Id = 2, Name = "Supreme", Price = 12.99m, Description = "Loaded with toppings", ImageName = "pizza_supreme.jpg" },
                    new Pizza { Id = 3, Name = "Hawaiian", Price = 11.99m, Description = "Ham and pineapple", ImageName = "pizza_hawaiian.jpg" },
                    new Pizza { Id = 4, Name = "BBQ Chicken", Price = 13.99m, Description = "BBQ sauce with chicken", ImageName = "pizza_bbqchicken.jpg" },
                    new Pizza { Id = 5, Name = "Pepperoni", Price = 12.99m, Description = "Pepperoni pizza", ImageName = "pizza_pepperoni.jpg" },
                    new Pizza { Id = 6, Name = "Veggie Delight", Price = 11.49m, Description = "Loaded with fresh vegetables", ImageName = "pizza_veggiedelight.jpg" },
                    new Pizza { Id = 7, Name = "Meat Lovers", Price = 13.49m, Description = "A hearty mix of various meats", ImageName = "pizza_meatlovers.jpg" },
                    new Pizza { Id = 8, Name = "Buffalo Chicken", Price = 14.49m, Description = "Spicy buffalo chicken pizza", ImageName = "pizza_buffalochicken.jpg" },
                    new Pizza { Id = 9, Name = "Four Cheese", Price = 12.49m, Description = "A blend of four delicious cheeses", ImageName = "pizza_fourcheese.jpg" },
                    new Pizza { Id = 10, Name = "Mediterranean", Price = 13.99m, Description = "With olives, feta, and sun-dried tomatoes", ImageName = "pizza_mediterranean.jpg" },
                    new Pizza { Id = 11, Name = "Pesto Chicken", Price = 14.99m, Description = "Pizza with a basil pesto sauce and chicken", ImageName = "pizza_pestochicken.jpg" },
                    new Pizza { Id = 12, Name = "Spinach Alfredo", Price = 13.49m, Description = "Creamy alfredo sauce with fresh spinach", ImageName = "pizza_spinachalfredo.jpg" },
                    new Pizza { Id = 13, Name = "BBQ Pulled Pork", Price = 15.49m, Description = "Topped with tender pulled pork in BBQ sauce", ImageName = "pizza_bbqpulledpork.jpg" },
                    new Pizza { Id = 14, Name = "Mexican Fiesta", Price = 14.49m, Description = "Spicy salsa, jalapeños, and guacamole", ImageName = "pizza_mexicanfiesta.jpg" },
                    new Pizza { Id = 15, Name = "Truffle Mushroom", Price = 16.99m, Description = "Wild mushrooms with a hint of truffle oil", ImageName = "pizza_trufflemushroom.jpg" }
                };

                await AddPizzasAsync(pizzas);
            }
        }

        internal async Task<Pizza> GetPizzaByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
