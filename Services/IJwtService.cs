using trans_api.Models; // Importing the User model namespace

namespace trans_api.Services
{
    public interface IJwtService // Define IJwtService interface
    {
        // Method to generate a JWT token for a user
        string GenerateToken(User user);
    }
}
