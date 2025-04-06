using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_App.Models
{
    // Represents a pizza in the application.
    public class Pizza
    {
        // Unique identifier for the pizza (optional if not needed).
        public int Id { get; set; }

        // Name of the pizza (e.g., "Margherita", "Pepperoni").
        public string Name { get; set; }

        // Price of the pizza.
        public decimal Price { get; set; }

        // A short description of the pizza.
        public string Description { get; set; }

        // The image file name for the pizza (e.g., "pizza_margherita.jpg").
        public string ImageName { get; set; }
    }
}

