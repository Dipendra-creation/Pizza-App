using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_App.Models
{
    using SQLite;

    namespace Pizza_App.Models
    {
        // Represents a user in the Pizza App.
        public class User
        {
            // Unique identifier for each user.
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            // Username for login (should be unique).
            public string Username { get; set; }

            // Email address of the user (should be unique).
            public string Email { get; set; }

            // User password
            public string Password { get; set; }

            // Full name of the user.
            public string FullName { get; set; }

            // Phone number for contact.
            public string PhoneNumber { get; set; }

            // Delivery address for orders.
            public string Address { get; set; }

            // Date and time when the user was registered.
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
    }
}


