using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarWash.BL.DTOs;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        /// <summary>
        /// Retrieves the top N leaderboard entries sorted by water saved.
        /// </summary>
        /// <param name="count">The number of top entries to retrieve.</param>
        /// <returns>A list of leaderboard entries.</returns>
        [HttpGet]
        public async Task<ActionResult<List<LeaderboardDto>>> GetLeaderboard([FromQuery] int count = 10)
        {
            if (count <= 0)
            {
                return BadRequest("Count must be greater than 0.");
            }

            var leaderboard = await _leaderboardService.GetTopAsync(count);
            return Ok(leaderboard);
        }
    }
}