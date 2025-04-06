using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_App.Models
{
    // Represents an item in the shopping cart.
    public class CartItem
    {
        // Unique identifier for the cart item.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Name of the pizza (e.g., "Pepperoni").
        public string PizzaName { get; set; }

        // Selected size for the pizza (e.g., "Small", "Medium", "Large").
        public string Size { get; set; }

        // Selected crust type (e.g., "Thin", "Thick").
        public string Crust { get; set; }

        // List of selected toppings, stored as a comma-separated string.
        public string Toppings { get; set; }

        // The unit price for the pizza.
        public decimal UnitPrice { get; set; }

        // Quantity of this pizza in the cart.
        public int Quantity { get; set; }

        // Total price for this cart item (usually UnitPrice * Quantity).
        public decimal TotalPrice { get; set; }
    }
}
