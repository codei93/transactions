using trans_api.DTOs; // Importing DTOs
using trans_api.Models; // Importing Models

namespace trans_api.Repositories
{
    // Interface for User repository, defining CRUD operations for users
    public interface IUserRepository
    {
        // Retrieves a user by its unique identifier asynchronously
        Task<User> GetUserByIdAsync(int id);

        // Retrieves a user by its username asynchronously
        Task<User> GetUserByUsernameAsync(string username);

        // Retrieves a user DTO by its email asynchronously
        Task<UserDTO> GetUserByEmailAsync(string email);

        // Retrieves all users asynchronously
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        // Adds a new user asynchronously
        Task AddUserAsync(User user);

        // Updates an existing user asynchronously
        Task UpdateUserAsync(User user);

        // Deletes a user by its unique identifier asynchronously
        Task DeleteUserAsync(int id);
    }
}
