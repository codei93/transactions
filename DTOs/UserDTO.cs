using System.ComponentModel.DataAnnotations;

namespace trans_api.DTOs
{
    // Represents Data Transfer Objects (DTOs) related to users
    public class UserDTO
    {
        // Properties to represent user information
        public int Id { get; set; } // Unique identifier for the user
        public string Username { get; set; } // Username of the user
        public string Email { get; set; } // Email address of the user
        public int RoleId { get; set; } // Role Id associated with the user
        public RoleDTO Role { get; set; } // Navigation property representing the role associated with the user
        public DateTime? CreatedAt { get; set; } // Timestamp indicating when the user was created

        // DTO for updating user information
        public class UpdateUserOTD
        {
            [Required] // Id is required to identify the user being updated
            public int Id { get; set; } // Unique identifier for the user

            [Required] // Username is required when updating user information
            public string Username { get; set; } // Username of the user

            [Required] // Email is required when updating user information
            public string Email { get; set; } // Email address of the user

            [Required] // RoleId is required when updating user information
            public int RoleId { get; set; } // Role Id associated with the user
        }

        // DTO for updating user password
        public class UpdatedPasswordOTD
        {
            [Required] // Username is required to identify the user whose password is being updated
            public required string Username { get; set; } // Username of the user

            [Required] // CurrentPassword is required when updating user password
            public required string CurrentPassword { get; set; } // Current password of the user

            [Required] // Password is required when updating user password
            public required string Password { get; set; } // New password for the user
        }
    }
}
