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
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        // Constructor to inject dependencies
        public AuthController(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        /// <summary>
        /// The endpoint logs in a user.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseExamples.UserLogin200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.User404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login(AuthDTO.LoginOTD login)
        {
            // Try to get the user by username
            var existingUser = await _userRepository.GetUserByUsernameAsync(login.Username);
            try
            {
                // If user does not exist, return 404 Not Found
                if (existingUser == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A user with the specified Username was not found"
                    });
                }

                // Verify password, if incorrect return 401 Unauthorized
                if (!BCrypt.Net.BCrypt.Verify(login.Password, existingUser.Password))
                {
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Wrong Credentials, try again."
                    });
                }

                // Get user details by email
                var authUser = await _userRepository.GetUserByEmailAsync(existingUser.Email);

                // Generate JWT token
                string tokenValue = _jwtService.GenerateToken(existingUser);

                // Return OK response with token and user details
                return Ok(new
                {
                    StatusCode = 200,
                    token = tokenValue,
                    user = authUser
                });
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error in case of exceptions
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint registers/creates the user.
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseExamples.UserRegistration200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.UserRegistration400Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(AuthDTO.RegisterOTD register)
        {
            // Check if user with same username or email already exists
            var existingUserByUsername = await _userRepository.GetUserByUsernameAsync(register.Username);
            var existingUserByEmail = await _userRepository.GetUserByEmailAsync(register.Email);

            try
            {
                // If user exists, return 400 Bad Request
                if (existingUserByUsername != null || existingUserByEmail != null)
                {
                    return BadRequest(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Bad request. A user with such details already exists."
                    });
                }

                // Create a new user and hash the password
                var newUser = new User
                {
                    Username = register.Username,
                    Email = register.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
                    RoleId = register.RoleId,
                };
                await _userRepository.AddUserAsync(newUser);

                // Return OK response indicating user creation
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User has been created."
                });
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error in case of exceptions
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Unexpected error occurred while processing your request.",
                    Detailed = ex.Message
                });
            }
        }

        /// <summary>
        /// The endpoint enable authenticated user to change his/her password.
        /// </summary>
        [Authorize]
        [HttpPut("updatePassword")]
        [ProducesResponseType(typeof(ResponseExamples.UserPassword200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.User404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response403), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseExamples.UserPassword400Response), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePassword(UserDTO.UpdatedPasswordOTD updatePassword)
        {
            // Get authenticated user ID and username from claims
            var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
            var authUsername = User.Claims.FirstOrDefault(c => c.Type == "AuthUsername")?.Value;

            try
            {
                // If authenticated username does not match provided username, return 401 Unauthorized
                if (authUsername != updatePassword.Username)
                {
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Authorization information is missing or invalid."
                    });
                }

                // Get user by ID
                var existingUser = await _userRepository.GetUserByIdAsync(int.Parse(authId));
                if (existingUser == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A user with the specified ID was not found"
                    });
                }

                // Verify current password, if incorrect return 400 Bad Request
                if (!BCrypt.Net.BCrypt.Verify(updatePassword.CurrentPassword, existingUser.Password))
                {
                    return BadRequest(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Bad request. Current password is not the same as your old password."
                    });
                }

                // Hash the new password and update user record
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updatePassword.Password);
                await _userRepository.UpdateUserAsync(existingUser);

                // Return OK response indicating password update
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User password has been updated."
                });
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error in case of exceptions
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
