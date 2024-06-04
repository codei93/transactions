using trans_api.DTOs; // Importing DTOs namespace

namespace trans_api.Services
{
    public interface IAnalysisService // Define IAnalysisService interface
    {
        // Method to get transaction graph analysis for a specific year
        Task<AnalysisDTO> GetTransactionGraphAnalysis(int currentYear, string role, int userId);

        // Method to calculate the sum of deposits
        Task<AnalysisDTO> SumDeposits(string role, int userId);

        // Method to calculate the sum of withdraws
        Task<AnalysisDTO> SumWithdraw(string role, int userId);

        // Method to count the number of users
        Task<AnalysisDTO> CountUsers();
    }
}
