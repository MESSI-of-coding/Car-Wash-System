using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.Domain.Models;
using System.Security.Claims;

namespace CarWash.API.Controllers
{
    [ApiController] // Mark the controller as an API controller
    [Route("api/cars")]
    [Authorize] // Require authentication for all endpoints
    [ApiExplorerSettings(GroupName = "v1")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Adds a new car for the current user.
        /// </summary>
        /// <param name="car">The car to add.</param>
        /// <returns>The added car.</returns>
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {
            // Safely parse user ID with null check
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID is missing in the token.");
            }
            var userId = int.Parse(userIdClaim);

            var addedCar = await _carService.AddCarAsync(car, userId);
            return CreatedAtAction(nameof(AddCar), new { id = addedCar.CarId }, addedCar);
        }

        /// <summary>
        /// Retrieves all cars for the current user.
        /// </summary>
        /// <returns>A list of cars associated with the current user.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            // Safely parse user ID with null check
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID is missing in the token.");
            }
            var userId = int.Parse(userIdClaim);

            var userCars = await _carService.GetCarsByUserIdAsync(userId);
            return Ok(userCars);
        }
    }
}