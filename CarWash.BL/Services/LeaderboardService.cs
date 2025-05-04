using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWash.BL.DTOs;
using CarWash.DAL.Repositories;
using CarWash.Domain.Models;

namespace CarWash.BL.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IWashRequestRepository _washRequestRepository;
        private readonly ILeaderboardRepository _leaderboardRepository;

        public LeaderboardService(IWashRequestRepository washRequestRepository, ILeaderboardRepository leaderboardRepository)
        {
            _washRequestRepository = washRequestRepository;
            _leaderboardRepository = leaderboardRepository;
        }

        public async Task UpdateDailyAsync()
        {
            var yesterday = DateTime.UtcNow.AddDays(-1);

            // Aggregate wash data for the past day
            var dailyData = await _washRequestRepository.GetDailyWashDataAsync(yesterday);

            foreach (var data in dailyData)
            {
                var leaderboardEntry = await _leaderboardRepository.GetByUserIdAsync(data.UserId);

                if (leaderboardEntry == null)
                {
                    // Insert new entry if it doesn't exist
                    leaderboardEntry = new Leaderboard
                    {
                        UserId = data.UserId,
                        TotalWaterSavedGallons = data.TotalWaterSavedGallons,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await _leaderboardRepository.AddAsync(leaderboardEntry);
                }
                else
                {
                    // Update existing entry
                    leaderboardEntry.TotalWaterSavedGallons += data.TotalWaterSavedGallons;
                    leaderboardEntry.UpdatedAt = DateTime.UtcNow;
                    await _leaderboardRepository.UpdateAsync(leaderboardEntry);
                }
            }
        }

        public async Task<List<LeaderboardDto>> GetTopAsync(int count)
        {
            var topEntries = await _leaderboardRepository.GetTopEntriesAsync(count);

            return topEntries.Select(entry => new LeaderboardDto
            {
                UserId = entry.UserId,
                TotalWaterSavedGallons = entry.TotalWaterSavedGallons,
                UpdatedAt = entry.UpdatedAt
            }).ToList();
        }
    }
}