using System.Threading.Tasks;
using Pizza_App.Models;
using Pizza_App.Models.Pizza_App.Models;
using SQLite;

namespace Pizza_App.Services
{
    // Provides methods for user authentication (signup and login) using SQLite.
    public class AuthenticationService
    {
        // Registers a new user in the database.
        public async Task<bool> SignUpAsync(User user)
        {
            // Check if the username or email already exists.
            var existingUser = await SQLiteService.Database.Table<User>()
                .Where(u => u.Username == user.Username || u.Email == user.Email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                // A user with the same username or email exists.
                return false;
            }

            // Insert the new user into the database.
            await SQLiteService.Database.InsertAsync(user);
            return true;
        }

        // Authenticates a user by username and password.
        public async Task<User> LoginAsync(string username, string password)
        {
            // In production, ensure to hash the password.
            var user = await SQLiteService.Database.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
