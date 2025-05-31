using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.Domain.Models;
using System.Security.Claims;
using AutoMapper; // Add AutoMapper namespace
using CarWash.BL.DTOs; // Add namespace for CarDto

namespace CarWash.API.Controllers
{
    [ApiController] // Mark the controller as an API controller
    [Route("api/cars")]
    [Authorize] // Require authentication for all endpoints
    [ApiExplorerSettings(GroupName = "v1")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new car for the current user.
        /// </summary>
        /// <param name="carDto">The car DTO to add.</param>
        /// <returns>The added car.</returns>
        [HttpPost("add-car")]
        public async Task<IActionResult> AddCar([FromBody] CarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto); // Map DTO to entity

            // Safely parse user ID with null check
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID is missing in the token.");
            }
            var userId = Guid.Parse(userIdClaim);

            var addedCar = await _carService.AddCarAsync(car, userId);
            return Ok(addedCar); // Removed CreatedAtAction
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
            var userId = Guid.Parse(userIdClaim);

            var userCars = await _carService.GetCarsByUserIdAsync(userId);
            return Ok(userCars);
        }
    }
}