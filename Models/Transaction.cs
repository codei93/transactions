namespace trans_api.Models
{
    // Represents a transaction entity
    public class Transaction
    {
        // Unique identifier for the transaction
        public int Id { get; set; }

        // Names of the customers involved in the transaction
        public required string CustomerNames { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Type of transaction (e.g., Deposit, Withdraw)
        public required string TransactionType { get; set; }  // Note: 'required' should be removed or corrected to 'Required'

        // Amount involved in the transaction
        public required int Amount { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Description of the transaction
        public required string Description { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Payment type of the transaction (e.g., MobileMoney, VisaCard)
        public required string PaymentType { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Identifier of the user associated with the transaction
        public required int UserId { get; set; } // Note: 'required' should be removed or corrected to 'Required'

        // Navigation property representing the user associated with the transaction
        public User? User { get; set; }

        // Date and time when the transaction was created
        public DateTime? CreatedAt { get; set; }

        // Date and time when the transaction was last updated
        public DateTime? UpdatedAt { get; set; }
    }
}
