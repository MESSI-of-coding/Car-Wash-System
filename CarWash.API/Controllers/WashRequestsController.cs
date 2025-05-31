using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.Domain.Models;
using CarWash.BL.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WashRequestsController : ControllerBase
    {
        private readonly IWashRequestService _washRequestService;
        private readonly IMapper _mapper;

        public WashRequestsController(IWashRequestService washRequestService, IMapper mapper)
        {
            _washRequestService = washRequestService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new wash request.
        /// </summary>
        /// <param name="createDto">The DTO containing details of the wash request to create.</param>
        /// <returns>Returns 201 Created with the created wash request, or 400 Bad Request if the creation fails.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateWashRequest([FromBody] CreateWashRequestDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest("Invalid request payload.");
            }

            // Map CreateWashRequestDto to WashRequest entity
            var washRequest = _mapper.Map<WashRequest>(createDto);

            var createdWashRequest = await _washRequestService.CreateWashRequestAsync(washRequest);

            if (createdWashRequest == null)
            {
                return BadRequest("Failed to create wash request.");
            }

            return Ok(createdWashRequest); // Removed CreatedAtAction
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWashRequestById(Guid id)
        {
            var result = await _washRequestService.GetWashRequestByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWashRequests()
        {
            var result = await _washRequestService.GetAllWashRequestsAsync();
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateWashRequestStatus(Guid id, [FromBody] string status)
        {
            var result = await _washRequestService.UpdateWashRequestStatusAsync(id, status);
            if (!result)
            {
                return BadRequest("Invalid status or request ID.");
            }
            return NoContent();
        }

        /// <summary>
        /// Retrieves all wash requests for the current user.
        /// </summary>
        /// <returns>Returns 200 OK with a list of wash requests, or 401 Unauthorized if the user ID is missing in the token.</returns>
        [HttpGet]
        public async Task<IActionResult> GetWashRequests()
        {
            // Read UserId from JWT claim
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID is missing in the token.");
            }

            var userId = Guid.Parse(userIdClaim);

            // Fetch wash requests via service
            var washRequests = await _washRequestService.GetWashRequestsByUserIdAsync(userId);

            // Return 200 OK with list of DTOs
            return Ok(washRequests);
        }
    }
}