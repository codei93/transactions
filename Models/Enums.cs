namespace trans_api.Models
{
    // Contains enumerations related to transactions
    public class Enums
    {
        // Enum representing different payment types
        public enum PaymentType
        {
            MobileMoney, // Payment via mobile money
            VisaCard     // Payment via Visa card
        }

        // Enum representing different transaction types
        public enum TransactionType
        {
            Deposit,  // Deposit transaction
            Withdraw  // Withdrawal transaction
        }
    }
}
