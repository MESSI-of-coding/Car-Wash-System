using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.Domain.Models;
using CarWash.BL.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WashRequestsController : ControllerBase
    {
        private readonly IWashRequestService _washRequestService;

        public WashRequestsController(IWashRequestService washRequestService)
        {
            _washRequestService = washRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWashRequest([FromBody] WashRequestDto requestDto)
        {
            // Validate foreign key references
            if (!await _washRequestService.IsValidCustomerId(requestDto.CustomerId))
            {
                return BadRequest("Invalid CustomerId.");
            }

            if (!await _washRequestService.IsValidCarId(requestDto.CarId))
            {
                return BadRequest("Invalid CarId.");
            }

            if (!await _washRequestService.IsValidPackageId(requestDto.PackageId))
            {
                return BadRequest("Invalid PackageId.");
            }

            var request = new WashRequest
            {
                CustomerId = requestDto.CustomerId,
                CarId = requestDto.CarId,
                PackageId = requestDto.PackageId,
                ScheduledDateTime = requestDto.ScheduledDateTime,
                Notes = requestDto.Notes
            };

            var result = await _washRequestService.CreateWashRequestAsync(request);
            return CreatedAtAction(nameof(GetWashRequestById), new { id = result.RequestId }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWashRequestById(int id)
        {
            var result = await _washRequestService.GetWashRequestByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWashRequests()
        {
            var result = await _washRequestService.GetAllWashRequestsAsync();
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateWashRequestStatus(int id, [FromBody] string status)
        {
            var result = await _washRequestService.UpdateWashRequestStatusAsync(id, status);
            if (!result)
            {
                return BadRequest("Invalid status or request ID.");
            }
            return NoContent();
        }
    }
}