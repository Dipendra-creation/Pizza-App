using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_App.Models
{
    // Represents an individual pizza item within an order.
    public class OrderItem
    {
        [PrimaryKey, AutoIncrement]
        public int OrderItemId { get; set; }

        // Foreign key linking this item to a specific order.
        public int OrderId { get; set; }

        // Name of the pizza (e.g., "Pepperoni").
        public string PizzaName { get; set; }

        // Selected size (e.g., "Small", "Medium", "Large").
        public string Size { get; set; }

        // Selected crust type (e.g., "Thin", "Thick").
        public string Crust { get; set; }

        // Comma-separated list of selected toppings.
        public string Toppings { get; set; }

        // Price for a single unit of the pizza.
        public decimal UnitPrice { get; set; }

        // Quantity of this pizza ordered.
        public int Quantity { get; set; }

        // Total price for this order item (UnitPrice * Quantity).
        public decimal TotalPrice { get; set; }
    }
}
