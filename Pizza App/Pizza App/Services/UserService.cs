using System.Threading.Tasks;
using Pizza_App.Models;
using Pizza_App.Models.Pizza_App.Models;
using SQLite;

namespace Pizza_App.Services
{
    // Service to manage user data operations using SQLite.
    public class UserService
    {
        // Reference to the SQLite database connection.
        private readonly SQLiteAsyncConnection _database;

        public UserService()
        {
            _database = SQLiteService.Database;
        }

        // Retrieves a user by username.
        public Task<User> GetUserAsync(string username)
        {
            return _database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }

        // Updates a user's profile information.
        public Task<int> UpdateUserAsync(User user)
        {
            return _database.UpdateAsync(user);
        }
    }
}
