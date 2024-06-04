using System.ComponentModel.DataAnnotations;

namespace trans_api.DTOs
{
    // Represents Data Transfer Objects (DTOs) related to roles
    public class RoleDTO
    {
        // Properties to represent role information
        public int Id { get; set; }     // Unique identifier for the role
        public string Name { get; set; } // Name of the role

        // DTO for creating a new role
        public class CreateRoleDTO
        {
            [Required] // Name is required when creating a role
            public string Name { get; set; } // Name of the role
        }

        // DTO for updating an existing role
        public class UpdateRoleDTO
        {
            [Required] // Id is required to identify the role being updated
            public int Id { get; set; } // Unique identifier for the role

            [Required] // Name is required when updating a role
            public string Name { get; set; } // Updated name of the role
        }
    }
}
