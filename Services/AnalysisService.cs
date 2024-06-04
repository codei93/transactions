using trans_api.Data; // Importing Data namespace
using trans_api.DTOs; // Importing DTOs namespace
using trans_api.Models; // Importing Models namespace
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core namespace

namespace trans_api.Services
{
    public class AnalysisService : IAnalysisService // Define AnalysisService class
    {
        private readonly ApplicationDbContext _context; // Declare private field for ApplicationDbContext

        // Constructor for AnalysisService class
        public AnalysisService(ApplicationDbContext context)
        {
            _context = context; // Initialize ApplicationDbContext through dependency injection
        }

        // Method to get transaction graph analysis for a specific year
        public async Task<AnalysisDTO> GetTransactionGraphAnalysis(int currentYear, string role, int userId)
        {
            // Initialize base query to filter transactions by year
            IQueryable<Transaction> query = _context.Transactions.Where(t => t.CreatedAt.Value.Year == currentYear);

            // Modify query based on user's role and ID
            if (role != "Admin")
            {
                query = query.Where(t => t.UserId == userId);
            }

            // Get monthly transaction counts for the specified year
            var monthlyTransactionCounts = await query
                .GroupBy(t => t.CreatedAt.Value.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToListAsync();

            // Prepare the counts for each month
            var monthlyCounts = new List<int>(new int[12]); // Initialize list to store counts for each month
            foreach (var item in monthlyTransactionCounts)
            {
                monthlyCounts[item.Month - 1] = item.Count; // Assign count to the corresponding month
            }

            // Return AnalysisDTO object with monthly transaction counts
            return new AnalysisDTO
            {
                MonthlyTransactionCounts = monthlyCounts,
            };
        }

        // Method to calculate the sum of deposits
        public async Task<AnalysisDTO> SumDeposits(string role, int userId)
        {
            // Initialize base query to filter transactions by deposit type
            IQueryable<Transaction> query = _context.Transactions.Where(t => t.TransactionType == "Deposit");

            // Modify query based on user's role and ID
            if (role != "Admin")
            {
                query = query.Where(t => t.UserId == userId);
            }

            // Get the sum of deposit amounts
            var sumDeposit = await query.SumAsync(t => t.Amount);

            // Return AnalysisDTO object with sum of deposits
            return new AnalysisDTO
            {
                SumDeposits = sumDeposit,
            };
        }

        // Method to calculate the sum of withdraws
        public async Task<AnalysisDTO> SumWithdraw(string role, int userId)
        {
            // Initialize base query to filter transactions by withdraw type
            IQueryable<Transaction> query = _context.Transactions.Where(t => t.TransactionType == "Withdraw");

            // Modify query based on user's role and ID
            if (role != "Admin")
            {
                query = query.Where(t => t.UserId == userId);
            }

            // Get the sum of withdraw amounts
            var sumWithdraws = await query.SumAsync(t => t.Amount);

            // Return AnalysisDTO object with sum of withdraws
            return new AnalysisDTO
            {
                SumWithdraws = sumWithdraws,
            };
        }

        // Method to count the number of users
        public async Task<AnalysisDTO> CountUsers()
        {
            // Get the total count of users
            var userCount = await _context.Users.CountAsync();

            // Return AnalysisDTO object with user count
            return new AnalysisDTO
            {
                UserCount = userCount,
            };
        }
    }
}
