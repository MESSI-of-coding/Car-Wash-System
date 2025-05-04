using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminUserController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Lists all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet("users")]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Activates or deactivates a user.
        /// </summary>
        /// <param name="id">The ID of the user to activate or deactivate.</param>
        /// <param name="action">The action to perform: 'activate' or 'deactivate'.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("users/{id}/{action}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromRoute] string action)
        {
            if (action != "activate" && action != "deactivate")
            {
                return BadRequest("Invalid action. Use 'activate' or 'deactivate'.");
            }

            var isActive = action == "activate";
            var result = await _userService.UpdateUserStatusAsync(id, isActive);

            if (!result)
            {
                return NotFound("User not found or status update failed.");
            }

            return NoContent();
        }
    }
}