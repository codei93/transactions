using trans_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using trans_api.DTOs;
using trans_api.Swagger;

namespace trans_api.Controllers
{
    // Define the route for the controller as "api/Analysis" and specify it is an API controller that produces JSON responses
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AnalysisController : ControllerBase
    {
        // Dependency injection for the analysis service
        private readonly IAnalysisService _analysisService;

        // Constructor to initialize the analysis service
        public AnalysisController(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        /// <summary>
        /// The endpoint retrieves the total monthly transactions by year of authenticated user
        /// </summary>
        /// <param name="year" example="2024">The year of transactions</param>
        [Authorize] // Ensure the user is authenticated
        [HttpGet("graph/{year}")] // Define the route and HTTP method
        [ProducesResponseType(typeof(IEnumerable<AnalysisDTO>), StatusCodes.Status200OK)] // Define possible response types
        [ProducesResponseType(typeof(ResponseExamples.Response403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTransactionsByYear(int year)
        {
            try
            {
                // Retrieve the authenticated user's ID and role from the claims
                var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
                var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;

                // Call the analysis service to get the transaction graph analysis data
                var graphData = await _analysisService.GetTransactionGraphAnalysis(year, authRole, int.Parse(authId));

                // Return the result with a status code of 200
                return Ok(new { StatusCode = 200, graphData });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an internal server error status code with the error details
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint retrieves the total sum of deposits of authenticated user
        /// </summary>
        [Authorize] // Ensure the user is authenticated
        [HttpGet("sum/deposits")] // Define the route and HTTP method
        [ProducesResponseType(typeof(IEnumerable<AnalysisDTO>), StatusCodes.Status200OK)] // Define possible response types
        [ProducesResponseType(typeof(ResponseExamples.Response403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSumDeposits()
        {
            // Retrieve the authenticated user's ID and role from the claims
            var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            try
            {
                // Call the analysis service to get the sum of deposits
                var deposits = await _analysisService.SumDeposits(authRole, int.Parse(authId));

                // Return the result with a status code of 200
                return Ok(new { StatusCode = 200, deposits });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an internal server error status code with the error details
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint retrieves the total sum of withdraws
        /// </summary>
        [Authorize] // Ensure the user is authenticated
        [HttpGet("sum/withdraws")] // Define the route and HTTP method
        [ProducesResponseType(typeof(IEnumerable<AnalysisDTO>), StatusCodes.Status200OK)] // Define possible response types
        [ProducesResponseType(typeof(ResponseExamples.Response403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSumWithdraws()
        {
            // Retrieve the authenticated user's ID and role from the claims
            var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            try
            {
                // Call the analysis service to get the sum of withdraws
                var withdraws = await _analysisService.SumWithdraw(authRole, int.Parse(authId));

                // Return the result with a status code of 200
                return Ok(new { StatusCode = 200, withdraws });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an internal server error status code with the error details
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint retrieves the total number of users 
        /// </summary>
        [Authorize] // Ensure the user is authenticated
        [HttpGet("count/users")] // Define the route and HTTP method
        [ProducesResponseType(typeof(IEnumerable<AnalysisDTO>), StatusCodes.Status200OK)] // Define possible response types
        [ProducesResponseType(typeof(ResponseExamples.Response403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCountUsers()
        {
            try
            {
                // Call the analysis service to get the total number of users
                var users = await _analysisService.CountUsers();

                // Return the result with a status code of 200
                return Ok(new { StatusCode = 200, users });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an internal server error status code with the error details
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
