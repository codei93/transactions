namespace trans_api.Models
{
    // Represents a user entity
    public class User
    {
        // Unique identifier for the user
        public int Id { get; set; }

        // Username of the user
        public required string Username { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Email address of the user
        public required string Email { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Password of the user
        public required string Password { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Role identifier of the user
        public required int RoleId { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Navigation property representing the role of the user
        public Role? Role { get; set; }

        // Date and time when the user was created
        public DateTime? CreatedAt { get; set; }

        // Date and time when the user was last updated
        public DateTime? UpdatedAt { get; set; }
    }
}
