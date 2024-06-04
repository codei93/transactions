using trans_api.DTOs; // Importing the DTOs namespace
using trans_api.Models; // Importing the Models namespace
using trans_api.Repositories; // Importing the Repositories namespace
using trans_api.Services; // Importing the Services namespace
using Microsoft.AspNetCore.Authorization; // Importing ASP.NET Core Authorization
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC
using System.Net; // Importing System.Net namespace
using trans_api.Swagger; // Importing Swagger namespace

namespace trans_api.Controllers
{
    // Sets the route for the controller and specifies it as an API controller
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        // Injecting dependencies
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IAuthorizeService _authorize;

        // Constructor to inject dependencies
        public UsersController(IUserRepository userRepository, IJwtService jwtService, IAuthorizeService authorize)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _authorize = authorize;
        }

        /// <summary>
        /// The endpoint gets a user by Id
        /// </summary>
        /// <param name="id" example="1">Id of the user</param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.UserGetOne200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.User404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserById(int id)
        {
            // Retrieve the user by Id
            var user = await _userRepository.GetUserByIdAsync(id);
            try
            {
                // If user is not found, return a 404 Not Found response
                if (user == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A user with the specified Username was not found."
                    });
                }

                // Return a successful response with the user data
                return Ok(new
                {
                    StatusCode = 200,
                    user
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
        /// The endpoint gets a user by Username
        /// </summary>
        /// <param name="username" example="aggrey">username of the user</param>
        [Authorize]
        [HttpGet("/username/{username}")]
        [ProducesResponseType(typeof(ResponseExamples.UserGetOne200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.User404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserByUsername(string username)
        {
            // Retrieve the user by username
            var user = await _userRepository.GetUserByUsernameAsync(username);
            try
            {
                // If user is not found, return a 404 Not Found response
                if (user == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A user with the specified Username was not found."
                    });
                }

                // Return a successful response with the user data
                return Ok(new
                {
                    StatusCode = 200,
                    user
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
        /// The endpoint gets all users
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResponseExamples.UserGetAll200Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                // Retrieve all users
                var users = await _userRepository.GetAllUsersAsync();

                // Return a successful response with the users data
                return Ok(new
                {
                    StatusCode = 200,
                    users
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
        /// The endpoint updates a user by Id
        /// </summary>
        /// <param name="id" example="1">Id of the user</param>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.UserUpdated200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.User404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.User400Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(int id, UserDTO.UpdateUserOTD updateUser)
        {
            // Get the authenticated user's ID and role from claims
            var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            // Retrieve the existing user by Id
            var exitingUser = await _userRepository.GetUserByIdAsync(id);

            try
            {
                // If user is not found, return a 404 Not Found response
                if (exitingUser == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A user with the specified Username was not found."
                    });
                }

                // If the Id in the URL does not match the Id in the request body, return a 400 Bad Request response
                if (id != updateUser.Id)
                {
                    return BadRequest(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Bad request. The userId does not match."
                    });
                }

                // Check if the authenticated user is authorized to update the user
                if (_authorize.HasRequiredPermissionAdmin(authRole) || int.Parse(authId) != id)
                {
                    // Update user properties
                    exitingUser.Username = updateUser.Username;
                    exitingUser.Email = updateUser.Email;
                    exitingUser.RoleId = updateUser.RoleId;

                    // Save the updated user to the repository
                    await _userRepository.UpdateUserAsync(exitingUser);

                    // Return a successful response
                    return Ok(new { StatusCode = 200, Message = "User has been updated!" });
                }
                else
                {
                    // Return an unauthorized response if the user is not authorized to update the user
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
        /// The endpoint deletes a user by Id
        /// </summary>
        /// <param name="id" example="1">Id of the user</param>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.UserDeleted200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.User404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            // Retrieve the existing user by Id
            var exitingUser = await _userRepository.GetUserByIdAsync(id);

            // Get the authenticated user's ID and role from claims
            var authId = User.Claims.FirstOrDefault(c => c.Type == "AuthId")?.Value;
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            try
            {
                // If user is not found, return a 404 Not Found response
                if (exitingUser == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A user with the specified Username was not found."
                    });
                }

                // Check if the authenticated user is authorized to delete the user
                if (_authorize.HasRequiredPermissionAdmin(authRole) || int.Parse(authId) != id)
                {
                    // Delete the user from the repository
                    await _userRepository.DeleteUserAsync(id);

                    // Return a successful response
                    return Ok(new { StatusCode = 200, Message = "User has been deleted." });
                }
                else
                {
                    // Return an unauthorized response if the user is not authorized to delete the user
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