using System.Net;
using Newtonsoft.Json;

namespace trans_api.Middleware
{
    // Middleware for handling exceptions globally
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next; // Reference to the next middleware component in the pipeline
        private readonly ILogger<ExceptionHandlingMiddleware> _logger; // Logger for logging exceptions

        // Constructor to initialize middleware dependencies
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next; // Store the reference to the next middleware component
            _logger = logger; // Store the logger instance
        }

        // Method to handle exceptions asynchronously
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Execute the next middleware component in the pipeline

                // If an exception occurs, control will be transferred to the catch block
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An unexpected error occurred!");

                // Handle the exception and send an appropriate response
                await HandleExceptionAsync(context, ex);
            }
        }

        // Method to handle exceptions and send an appropriate response
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set response content type to JSON
            context.Response.ContentType = "application/json";

            // Set the response status code to 500 (Internal Server Error)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Create an anonymous object representing the error response
            var response = new
            {
                StatusCode = context.Response.StatusCode, // Status code of the response
                Message = "An internal server error occurred. Please try again later.", // Error message
                Detailed = exception.Message // Detailed error message (remove or limit this in production for security reasons)
            };

            // Convert the error response to JSON and write it to the response stream
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
