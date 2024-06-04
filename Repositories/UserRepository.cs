using trans_api.Data; // Importing Data namespace
using trans_api.Models; // Importing Models namespace
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core
using trans_api.DTOs; // Importing DTOs namespace

namespace trans_api.Repositories
{
    // Repository class for handling User-related database operations
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; // Application database context

        // Constructor to initialize the repository with the database context
        public UserRepository(ApplicationDbContext context)
        {
            _context = context; // Assigning the provided database context to the local variable
        }

        // Retrieves a user by their unique identifier asynchronously, including the associated role
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync((u) => u.Id == id);
        }

        // Retrieves a user by their username asynchronously, including the associated role
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync((u) => u.Username == username);
        }

        // Retrieves a user DTO by their email asynchronously, mapping it to a DTO
        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Select(u => new UserDTO // Mapping User entity to UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = new RoleDTO // Mapping associated Role entity to RoleDTO
                {
                    Id = u.Role.Id,
                    Name = u.Role.Name
                }
            }).FirstOrDefaultAsync((u) => u.Email == email);
        }

        // Retrieves all users asynchronously, mapping them to DTOs
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await _context.Users.OrderByDescending(u => u.CreatedAt).Select(u => new UserDTO // Mapping each user to UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                RoleId = u.RoleId,

                Role = new RoleDTO // Mapping associated Role entity to RoleDTO
                {
                    Id = u.Role.Id,
                    Name = u.Role.Name
                }
            }).ToListAsync();
        }

        // Adds a new user asynchronously
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user); // Adding new user to the database
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Updates an existing user asynchronously
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user); // Marking user as updated
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Deletes a user by their unique identifier asynchronously
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id); // Finding the user by their ID

            if (user != null)
            {
                _context.Users.Remove(user); // Removing the user from the database
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }
    }
}
