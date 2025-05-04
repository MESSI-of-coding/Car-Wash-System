using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/admin/washers")]
    [Authorize(Roles = "Admin")]
    public class AdminWasherController : ControllerBase
    {
        private readonly IWasherService _washerService;

        public AdminWasherController(IWasherService washerService)
        {
            _washerService = washerService;
        }

        /// <summary>
        /// Lists all washers.
        /// </summary>
        /// <returns>A list of all washers.</returns>
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllWashers()
        {
            var washers = await _washerService.GetAllWashersAsync();
            return Ok(washers);
        }

        /// <summary>
        /// Updates washer details.
        /// </summary>
        /// <param name="id">The ID of the washer to update.</param>
        /// <param name="washer">The updated washer details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWasher(int id, [FromBody] User washer)
        {
            var result = await _washerService.UpdateWasherAsync(id, washer);

            if (!result)
            {
                return NotFound("Washer not found or update failed.");
            }

            return NoContent();
        }

        /// <summary>
        /// Soft deletes a washer.
        /// </summary>
        /// <param name="id">The ID of the washer to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteWasher(int id)
        {
            var result = await _washerService.SoftDeleteWasherAsync(id);

            if (!result)
            {
                return NotFound("Washer not found or delete failed.");
            }

            return NoContent();
        }
    }
}