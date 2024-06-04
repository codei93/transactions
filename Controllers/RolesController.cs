using trans_api.Models;
using trans_api.Repositories;
using trans_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using trans_api.DTOs;
using trans_api.Swagger;

namespace trans_api.Controllers
{
    // Sets the route for the controller and specifies it as an API controller
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthorizeService _authorize;

        // Constructor to inject dependencies
        public RolesController(IRoleRepository roleRepository, IAuthorizeService authorize)
        {
            _roleRepository = roleRepository;
            _authorize = authorize;
        }

        /// <summary>
        /// The endpoint creates a role
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseExamples.RoleCreate200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.RoleCreate400Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Role>> CreateRole(RoleDTO.CreateRoleDTO role)
        {
            // Get the authenticated user's role from claims
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            // Check if the role already exists
            var existingRole = await _roleRepository.GetRoleByNameAsync(role.Name);

            try
            {
                // If the role exists, return a 400 Bad Request response
                if (existingRole != null)
                {
                    return BadRequest(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Bad request. A Role Name with such details already exists."
                    });
                }

                // Check if the authenticated user has the required admin permissions
                if (_authorize.HasRequiredPermissionAdmin(authRole))
                {
                    var newRole = new Role { Name = role.Name };

                    await _roleRepository.AddRoleAsync(newRole);

                    return Ok(new { StatusCode = 200, Message = "Role has been Created" });
                }
                else
                {
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Authorization information is missing or invalid."
                    });
                }
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
        /// The endpoint gets a role by Id
        /// </summary>
        /// <param name="id" example="1">Id of the role</param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.RoleGetOne200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Role404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRoleById(int id)
        {
            // Get the authenticated user's role from claims
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            try
            {
                // Get the role by ID
                var role = await _roleRepository.GetRoleByIdAsync(id);
                if (role == null)
                {
                    // If the role is not found, return a 404 Not Found response
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A role with the specified Id was not found."
                    });
                }

                // Check if the authenticated user has the required admin permissions
                if (_authorize.HasRequiredPermissionAdmin(authRole))
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        role
                    });
                }
                else
                {
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Authorization information is missing or invalid."
                    });
                }
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
        /// The endpoint gets all roles
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResponseExamples.RoleGetAll200Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            // Get the authenticated user's role from claims
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            try
            {
                // Check if the authenticated user has the required admin permissions
                if (_authorize.HasRequiredPermissionAdmin(authRole))
                {
                    var roles = await _roleRepository.GetAllRolesAsync();
                    return Ok(new { StatusCode = 200, roles });
                }
                else
                {
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Authentication information is missing or invalid."
                    });
                }
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
        /// The endpoint updates a role by Id
        /// </summary>
        /// <param name="id" example="1">Id of the role</param>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.RoleUpdate200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Role404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.RoleUpdate400Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRole(int id, RoleDTO.UpdateRoleDTO role)
        {
            // Get the authenticated user's role from claims
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;
            // Get the role by ID
            var exitingRole = await _roleRepository.GetRoleByIdAsync(id);

            try
            {
                // If the role is not found, return a 404 Not Found response
                if (exitingRole == null)
                {
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A role with the specified Id was not found."
                    });
                }

                // If the role ID in the URL does not match the ID in the request body, return a 400 Bad Request response
                if (id != role.Id)
                {
                    return BadRequest(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Bad request. Role Id does not match."
                    });
                }

                // Check if the authenticated user has the required admin permissions
                if (_authorize.HasRequiredPermissionAdmin(authRole))
                {
                    exitingRole.Name = role.Name;
                    await _roleRepository.UpdateRoleAsync(exitingRole);

                    return Ok(new { StatusCode = 200, Message = "Role has been updated." });
                }
                else
                {
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Authentication information is missing or invalid."
                    });
                }
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
        /// The endpoint deletes a role by Id.
        /// </summary>
        /// <param name="id" example="1">Id of the role</param>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseExamples.RoleDelete200Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseExamples.Role404Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseExamples.RoleUpdate400Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseExamples.Response401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseExamples.Response500), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteRole(int id)
        {
            // Get the authenticated user's role from claims
            var authRole = User.Claims.FirstOrDefault(c => c.Type == "AuthRole")?.Value;

            try
            {
                // Get the role by ID
                var exitingRole = await _roleRepository.GetRoleByIdAsync(id);
                if (exitingRole == null)
                {
                    // If the role is not found, return a 404 Not Found response
                    return NotFound(new
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "A role with the specified Id was not found."
                    });
                }

                // Check if the authenticated user has the required admin permissions
                if (_authorize.HasRequiredPermissionAdmin(authRole))
                {
                    await _roleRepository.DeleteRoleAsync(id);

                    return Ok(new { StatusCode = 200, Message = "Role has been deleted." });
                }
                else
                {
                    return Unauthorized(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Authentication information is missing or invalid."
                    });
                }
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
