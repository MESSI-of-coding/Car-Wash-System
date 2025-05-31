using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash.Domain.Models;

namespace CarWash.DAL.Repositories
{
    public interface ILeaderboardRepository
    {
        Task<Leaderboard> GetByUserIdAsync(Guid userId); // Retrieve leaderboard entry by user ID
        Task AddAsync(Leaderboard leaderboardEntry); // Add a new leaderboard entry
        Task UpdateAsync(Leaderboard leaderboardEntry); // Update an existing leaderboard entry
        Task<List<Leaderboard>> GetTopEntriesAsync(int count); // Retrieve top N leaderboard entries
    }
}