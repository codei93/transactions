using trans_api.DTOs;
using trans_api.Models;
using trans_api.Repositories;
using trans_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using trans_api.Swagger;

namespace trans_api.Controllers
{
    // Sets the route for the controller and specifies it as an API controller
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TransactionsController : ControllerBase
    {
        // Injecting the dependencies
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAuthorizeService _authorize;

        // Constructor to inject dependencies
        public TransactionsController(ITransactionRepository transactionRepository, IAuthorizeService authorize)
        {
            _transactionRepository = transactionRepository;
            _authorize = authorize;
        }

        /// <summary>
        /// The endpoint creates a transaction
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseExamples.TransactionCreate200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransactionDTO.CreateTransactionOTD>> CreateTransaction(TransactionDTO.CreateTransactionOTD transaction)
        {
            // Get the authenticated user's ID from claims
            var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
            try
            {
                // Create a new transaction object
                var newTransaction = new Transaction
                {
                    CustomerNames = transaction.CustomerNames,
                    TransactionType = transaction.TransactionType,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    PaymentType = transaction.PaymentType,
                    UserId = int.Parse(authId),
                };

                // Add the new transaction to the repository
                await _transactionRepository.AddTransactionAsync(newTransaction);

                // Return a successful response
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Transaction has been created."
                });
            }
            catch (Exception ex)
            {
                // Return an internal server error response in case of exceptions
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint gets a transaction by Id
        /// </summary>
        /// <param name="id" example="1">Id of the transaction</param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.TransactionGetOne200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Transaction404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTransactionById(int id)
        {
            // Get the transaction by ID
            var transaction = await _transactionRepository.GetTransactionDTOByIdAsync(id);
            try
            {
                // If the transaction is not found, return a 404 Not Found response
                if (transaction == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A transaction with the specified Id was not found."
                    });
                }
                // Return a successful response with the transaction data
                return Ok(new { StatusCode = 200, transaction });
            }
            catch (Exception ex)
            {
                // Return an internal server error response in case of exceptions
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint gets all transactions
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResponseExamples.TransactionGetAll200Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            try
            {
                // Get the authenticated user's ID and role from claims
                var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
                var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;

                // Get all transactions based on user's role and ID
                var transactions = await _transactionRepository.GetAllTransactionAsync(authRole, int.Parse(authId));

                // Return a successful response with the transactions data
                return Ok(new { StatusCode = 200, transactions });
            }
            catch (Exception ex)
            {
                // Return an internal server error response in case of exceptions
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint updates a transaction by Id
        /// </summary>
        /// <param name="id" example="1">Id of the transaction</param>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.TransactionUpdate200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Transaction404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Transaction400Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTransaction(int id, TransactionDTO.UpdateTransactionOTD transaction)
        {
            // Get the authenticated user's ID and role from claims
            var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            // Get the existing transaction by ID
            var exitingTransaction = await _transactionRepository.GetTransactionByIdAsync(id);
            try
            {
                // If the transaction is not found, return a 404 Not Found response
                if (exitingTransaction == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A transaction with the specified Id was not found."
                    });
                }

                // If the ID in the URL does not match the ID in the request body, return a 400 Bad Request response
                if (id != transaction.Id)
                {
                    return BadRequest(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Bad request. Transaction Id does not match."
                    });
                }

                // Check if the authenticated user is authorized to update the transaction
                if (int.Parse(authId) == exitingTransaction.UserId || _authorize.HasRequiredPermissionAdmin(authRole))
                {
                    // Update the transaction properties
                    exitingTransaction.CustomerNames = transaction.CustomerNames;
                    exitingTransaction.TransactionType = transaction.TransactionType;
                    exitingTransaction.Amount = transaction.Amount;
                    exitingTransaction.Description = transaction.Description;
                    exitingTransaction.PaymentType = transaction.PaymentType;

                    // Save the updated transaction to the repository
                    await _transactionRepository.UpdateTransationAsync(exitingTransaction);

                    // Return a successful response
                    return Ok(new { StatusCode = 200, Message = "Transaction has been updated." });
                }
                else
                {
                    // Return an unauthorized response if the user is not authorized to update the transaction
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Authentication information is missing or invalid."
                    });
                }
            }
            catch (Exception ex)
            {
                // Return an internal server error response in case of exceptions
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint deletes a transaction by Id
        /// </summary>
        /// <param name="id" example="1">Id of the transaction</param>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.TransactionDelete200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Transaction404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            // Get the authenticated user's ID and role from claims
            var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            try
            {
                // Get the existing transaction by ID
                var exitingTransaction = await _transactionRepository.GetTransactionByIdAsync(id);
                if (exitingTransaction == null)
                {
                    // If the transaction is not found, return a 404 Not Found response
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A transaction with the specified Id was not found."
                    });
                }

                // Check if the authenticated user is authorized to delete the transaction
                if (int.Parse(authId) == exitingTransaction.UserId || _authorize.HasRequiredPermissionAdmin(authRole))
                {
                    // Delete the transaction from the repository
                    await _transactionRepository.DeleteTransactionAsync(id);

                    // Return a successful response
                    return Ok(new { StatusCode = 200, Message = "Transaction has been deleted." });
                }
                else
                {
                    // Return an unauthorized response if the user is not authorized to delete the transaction
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Authentication information is missing or invalid."
                    });
                }
            }
            catch (Exception ex)
            {
                // Return an internal server error response in case of exceptions
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }
    }
}