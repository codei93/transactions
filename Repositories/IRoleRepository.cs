using trans_api.Models; // Importing the Role model

namespace trans_api.Repositories
{
    // Interface for Role repository, defining CRUD operations for roles
    public interface IRoleRepository
    {
        // Retrieves a role by its unique identifier asynchronously
        Task<Role> GetRoleByIdAsync(int id);

        // Retrieves a role by its name asynchronously
        Task<Role> GetRoleByNameAsync(string name);

        // Retrieves all roles asynchronously
        Task<IEnumerable<Role>> GetAllRolesAsync();

        // Adds a new role asynchronously
        Task AddRoleAsync(Role role);

        // Updates an existing role asynchronously
        Task UpdateRoleAsync(Role role);

        // Deletes a role by its unique identifier asynchronously
        Task DeleteRoleAsync(int id);
    }
}