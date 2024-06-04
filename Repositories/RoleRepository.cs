using trans_api.Data; // Importing Data namespace
using trans_api.Models; // Importing Models namespace
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core
using trans_api.Repositories; // Importing Repositories namespace

namespace trans_api.Repositories
{
    // Repository class for handling Role-related database operations
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context; // Application database context

        // Constructor to initialize the repository with the database context
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context; // Assigning the provided database context to the local variable
        }

        // Retrieves a role by its unique identifier asynchronously
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id); // Find the role by its ID in the database
        }

        // Retrieves a role by its name asynchronously
        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync((r) => r.Name == name); // Find the role by its name in the database
        }

        // Retrieves all roles asynchronously
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.OrderByDescending(r => r.CreatedAt).ToListAsync(); // Retrieve all roles ordered by creation date
        }

        // Adds a new role asynchronously
        public async Task AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role); // Add the new role to the database
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Updates an existing role asynchronously
        public async Task UpdateRoleAsync(Role role)
        {
            var existingRole = await _context.Roles.FindAsync(role.Id); // Find the existing role by its ID

            if (existingRole != null)
            {
                _context.Entry(existingRole).State = EntityState.Detached; // Detach the existing role
            }

            _context.Attach(role); // Attach the updated role
            _context.Entry(role).State = EntityState.Modified; // Mark the role as modified
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Deletes a role by its unique identifier asynchronously
        public async Task DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id); // Find the role by its ID

            if (role != null)
            {
                _context.Roles.Remove(role); // Remove the role from the database
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }
    }
}
