using CarWash.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWash.DAL.Repositories
{
    public interface IWashRequestRepository
    {
        Task<WashRequest> AddAsync(WashRequest request);
        Task<WashRequest> GetByIdAsync(Guid id);
        Task<List<WashRequest>> GetAllAsync();
        Task UpdateAsync(WashRequest request);
        Task<bool> HasOverlappingScheduleAsync(Guid carId, DateTime scheduledDateTime);
        Task<IEnumerable<WashRequest>> GetWashRequestsByUserIdAsync(Guid userId);
        Task<List<DailyWashData>> GetDailyWashDataAsync(DateTime date); // Retrieves daily wash data for leaderboard updates
    }
}