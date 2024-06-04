using System.ComponentModel.DataAnnotations;

namespace trans_api.DTOs
{
    // Defines Data Transfer Objects (DTOs) for authentication-related data
    public class AuthDTO
    {
        // DTO for user login data
        public class LoginOTD
        {
            // Username is required for login
            [Required]
            public required string Username { get; set; }

            // Password is required for login
            [Required]
            public required string Password { get; set; }
        }

        // DTO for user registration data
        public class RegisterOTD
        {
            // Username is required for registration
            [Required]
            public string Username { get; set; }

            // Email is required for registration and must be a valid email address
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            // Password is required for registration
            [Required]
            public string Password { get; set; }

            // RoleId is required for registration to specify the role of the user
            [Required]
            public int RoleId { get; set; }
        }
    }
}
