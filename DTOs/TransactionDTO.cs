using System.ComponentModel.DataAnnotations;

namespace trans_api.DTOs
{
    // Represents Data Transfer Objects (DTOs) related to transactions
    public class TransactionDTO
    {
        // Properties to represent transaction information
        public int Id { get; set; } // Unique identifier for the transaction
        public string CustomerNames { get; set; } // Names of the customers involved in the transaction
        public string TransactionType { get; set; } // Type of transaction (e.g., deposit, withdrawal)
        public int Amount { get; set; } // Amount involved in the transaction
        public string Description { get; set; } // Description of the transaction
        public string PaymentType { get; set; } // Payment type used in the transaction

        // Navigation property representing the user associated with the transaction
        public UserDTO User { get; set; }

        // Properties representing the creation and update timestamps of the transaction
        public DateTime? CreatedAt { get; set; } // Timestamp indicating when the transaction was created
        public DateTime? UpdatedAt { get; set; } // Timestamp indicating when the transaction was last updated

        // DTO for creating a new transaction
        public class CreateTransactionOTD
        {
            [Required] // Customer names are required when creating a transaction
            public string CustomerNames { get; set; } // Names of the customers involved in the transaction

            [Required] // Transaction type is required when creating a transaction
            public string TransactionType { get; set; } // Type of transaction

            [Required] // Amount is required when creating a transaction
            public int Amount { get; set; } // Amount involved in the transaction

            [Required] // Description is required when creating a transaction
            public string Description { get; set; } // Description of the transaction

            [Required] // Payment type is required when creating a transaction
            public string PaymentType { get; set; } // Payment type used in the transaction
        }

        // DTO for updating an existing transaction
        public class UpdateTransactionOTD
        {
            [Required] // Id is required to identify the transaction being updated
            public int Id { get; set; } // Unique identifier for the transaction

            [Required] // Customer names are required when updating a transaction
            public string CustomerNames { get; set; } // Names of the customers involved in the transaction

            [Required] // Transaction type is required when updating a transaction
            public string TransactionType { get; set; } // Type of transaction

            [Required] // Amount is required when updating a transaction
            public int Amount { get; set; } // Amount involved in the transaction

            [Required] // Description is required when updating a transaction
            public string Description { get; set; } // Description of the transaction

            [Required] // Payment type is required when updating a transaction
            public string PaymentType { get; set; } // Payment type used in the transaction
        }
    }
}
