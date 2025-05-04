using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash.BL.DTOs;

namespace CarWash.BL.Services
{
    public interface ILeaderboardService
    {
        Task UpdateDailyAsync(); // Updates the leaderboard daily
        Task<List<LeaderboardDto>> GetTopAsync(int count); // Retrieves the top leaderboard entries
    }
}