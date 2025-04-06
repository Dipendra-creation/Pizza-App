using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_App.Models
{
    // Represents an order placed by the user.
    public class Order
    {
        // Unique identifier for the order.
        [PrimaryKey, AutoIncrement]
        public int OrderId { get; set; }

        // Foreign key referencing the User who placed the order.
        public int UserId { get; set; }

        // The date and time when the order was placed.
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // Total amount for the order.
        public decimal TotalAmount { get; set; }

        // Status of the order (e.g., Delivered, In Progress, Canceled).
        public string Status { get; set; }

        // Optional: A JSON-formatted string containing details of each item in the order.
        public string OrderDetails { get; set; }
    }
}
