using trans_api.DTOs; // Importing DTOs
using trans_api.Models; // Importing Models

namespace trans_api.Repositories
{
    // Interface for Transaction repository, defining CRUD operations for transactions
    public interface ITransactionRepository
    {
        // Retrieves a transaction by its unique identifier asynchronously
        Task<Transaction> GetTransactionByIdAsync(int id);

        // Retrieves a transaction DTO by its unique identifier asynchronously
        Task<TransactionDTO> GetTransactionDTOByIdAsync(int id);

        // Retrieves all transactions asynchronously based on the user's role and ID
        Task<IEnumerable<TransactionDTO>> GetAllTransactionAsync(string role, int userId);

        // Adds a new transaction asynchronously
        Task AddTransactionAsync(Transaction transaction);

        // Updates an existing transaction asynchronously
        Task UpdateTransationAsync(Transaction transaction);

        // Deletes a transaction by its unique identifier asynchronously
        Task DeleteTransactionAsync(int id);
    }
}
