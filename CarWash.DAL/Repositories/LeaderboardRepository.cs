using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWash.Domain.Models;
using Microsoft.EntityFrameworkCore;
using CarWash.DAL.Data;

namespace CarWash.DAL.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        private readonly AppDbContext _context;

        public LeaderboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Leaderboard> GetByUserIdAsync(int userId)
        {
            return await _context.Leaderboard.FirstOrDefaultAsync(l => l.UserId == userId);
        }

        public async Task AddAsync(Leaderboard leaderboardEntry)
        {
            await _context.Leaderboard.AddAsync(leaderboardEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Leaderboard leaderboardEntry)
        {
            _context.Leaderboard.Update(leaderboardEntry);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Leaderboard>> GetTopEntriesAsync(int count)
        {
            return await _context.Leaderboard
                .OrderByDescending(l => l.TotalWaterSavedGallons)
                .Take(count)
                .ToListAsync();
        }
    }
}