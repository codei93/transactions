using trans_api.Models; // Importing the User model namespace
using Microsoft.IdentityModel.Tokens; // Importing necessary namespaces for JWT token generation
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace trans_api.Services
{
    public class JwtService : IJwtService // Define JwtService class implementing IJwtService interface
    {
        private readonly IConfiguration _configuration; // Configuration object to access JWT configuration

        // Constructor to inject IConfiguration dependency
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration; // Assigning injected IConfiguration instance to local variable
        }

        // Method to generate JWT token for a given user
        public string GenerateToken(User user)
        {
            // Define claims for the JWT token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("AuthId", user.Id.ToString()),
                new Claim("AuthRole", user.Role.Name.ToString()),
                new Claim("AuthUsername", user.Username.ToString()),
                new Claim("AuthEmail", user.Email.ToString())
            };

            // Create a symmetric security key using the configured key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Create signing credentials using the key and HMACSHA256 algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create a JWT token with specified issuer, audience, claims, expiry, and signing credentials
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(720), // Token expiry time
                signingCredentials: creds
            );

            // Write the JWT token as a string and return
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
