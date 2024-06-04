using trans_api.Data; // Importing Data namespace
using trans_api.Models; // Importing Models namespace
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core
using trans_api.DTOs; // Importing DTOs namespace

namespace trans_api.Repositories
{
    // Repository class for handling Transaction-related database operations
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context; // Application database context

        // Constructor to initialize the repository with the database context
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context; // Assigning the provided database context to the local variable
        }

        // Retrieves a transaction DTO by its unique identifier asynchronously
        public async Task<TransactionDTO> GetTransactionDTOByIdAsync(int id)
        {
            return await _context.Transactions
                .Select(t => new TransactionDTO // Mapping Transaction entity to TransactionDTO
                {
                    Id = t.Id,
                    CustomerNames = t.CustomerNames,
                    TransactionType = t.TransactionType,
                    Amount = t.Amount,
                    Description = t.Description,
                    PaymentType = t.PaymentType,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,

                    User = new UserDTO // Mapping associated User entity to UserDTO
                    {
                        Id = t.User.Id,
                        Username = t.User.Username,
                        Email = t.User.Email,
                        RoleId = t.User.RoleId
                    }
                }).FirstOrDefaultAsync((t) => t.Id == id);
        }

        // Retrieves a transaction by its unique identifier asynchronously
        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.Include(t => t.User).FirstOrDefaultAsync((t) => t.Id == id); // Include associated User entity
        }

        // Retrieves all transactions asynchronously based on user role and ID
        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionAsync(string role, int userId)
        {
            IQueryable<Transaction> query = _context.Transactions.OrderByDescending(t => t.CreatedAt); // Ordering transactions by creation date

            if (role != "Admin")
            {
                query = query.Where(t => t.UserId == userId); // Filtering transactions based on user ID if not admin
            }

            return await query.Select(t => new TransactionDTO // Mapping each transaction to TransactionDTO
            {
                Id = t.Id,
                CustomerNames = t.CustomerNames,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                Description = t.Description,
                PaymentType = t.PaymentType,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,

                User = new UserDTO // Mapping associated User entity to UserDTO
                {
                    Id = t.User.Id,
                    Username = t.User.Username,
                    Email = t.User.Email,
                    RoleId = t.User.RoleId
                }
            }).ToListAsync();
        }

        // Adds a new transaction asynchronously
        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction); // Adding new transaction to the database
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Updates an existing transaction asynchronously
        public async Task UpdateTransationAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction); // Marking transaction as updated
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Deletes a transaction by its unique identifier asynchronously
        public async Task DeleteTransactionAsync(int id)
        {
            var transactions = await _context.Transactions.FindAsync(id); // Finding the transaction by its ID

            if (transactions != null)
            {
                _context.Transactions.Remove(transactions); // Removing the transaction from the database
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }
    }
}
