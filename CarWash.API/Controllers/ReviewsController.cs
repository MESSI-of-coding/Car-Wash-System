using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.BL.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Submits a review for a completed wash request.
        /// </summary>
        /// <param name="dto">The review details including RequestId, Rating, and Comments.</param>
        /// <returns>201 Created if successful, 400 Bad Request if validation fails.</returns>
        [HttpPost]
        public async Task<IActionResult> SubmitReview([FromBody] CreateReviewDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _reviewService.SubmitReviewAsync(dto);
            return Ok(new { Message = "Review submitted successfully." });
        }

        /// <summary>
        /// Retrieves all reviews for a specific washer.
        /// </summary>
        /// <param name="washerId">The ID of the washer.</param>
        /// <returns>A list of reviews for the specified washer.</returns>
        [HttpGet("{washerId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetReviewsForWasher(Guid washerId)
        {
            var reviews = await _reviewService.GetReviewsForWasherAsync(washerId);
            return Ok(reviews);
        }
    }
}