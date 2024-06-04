using System.ComponentModel.DataAnnotations;

namespace trans_api.Models
{
    // Represents a user role
    public class Role
    {
        // Unique identifier for the role
        public int Id { get; set; }

        // Name of the role
        [Required] // Specifies that the Name property is required
        public string Name { get; set; }

        // Date and time when the role was created
        public DateTime CreatedAt { get; set; }

        // Date and time when the role was last updated
        public DateTime UpdatedAt { get; set; }
    }
}
