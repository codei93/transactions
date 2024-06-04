namespace trans_api.DTOs
{
    // Defines a Data Transfer Object (DTO) for analysis-related data
    public class AnalysisDTO
    {
        // A list representing the count of transactions for each month
        public List<int> MonthlyTransactionCounts { get; set; }

        // The total sum of deposit transactions
        public int SumDeposits { get; set; }

        // The total sum of withdrawal transactions
        public int SumWithdraws { get; set; }

        // The total count of users
        public int UserCount { get; set; }
    }
}