using CarWash.Domain.Models;
using CarWash.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CarWash.DAL.Repositories
{
    public class WashRequestRepository : IWashRequestRepository
    {
        private readonly AppDbContext _context;

        public WashRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WashRequest> AddAsync(WashRequest request)
        {
            await _context.WashRequests.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<WashRequest> GetByIdAsync(Guid id)
        {
            return await _context.WashRequests.FindAsync(id);
        }

        public async Task<List<WashRequest>> GetAllAsync()
        {
            return await _context.WashRequests.ToListAsync();
        }

        public async Task UpdateAsync(WashRequest request)
        {
            _context.WashRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasOverlappingScheduleAsync(Guid carId, DateTime scheduledDateTime)
        {
            return await _context.WashRequests.AnyAsync(wr =>
                wr.CarId == carId &&
                wr.ScheduledDateTime == scheduledDateTime &&
                wr.Status != WashStatus.Cancelled);
        }

        public async Task<IEnumerable<WashRequest>> GetWashRequestsByUserIdAsync(Guid userId)
        {
            return await _context.WashRequests
                .Where(wr => wr.CustomerId == userId)
                .ToListAsync();
        }

        public async Task<List<DailyWashData>> GetDailyWashDataAsync(DateTime date)
        {
            return await _context.WashRequests
                .Where(wr => wr.Status == WashStatus.Completed && wr.ActualWashDateTime.HasValue && wr.ActualWashDateTime.Value.Date == date.Date)
                .GroupBy(wr => wr.CustomerId)
                .Select(group => new DailyWashData
                {
                    UserId = group.Key,
                    TotalWaterSavedGallons = group.Sum(wr => wr.Package.WaterSavedGallons) // Water Saved Calculation
                })
                .ToListAsync();
        }
    }

    public class DailyWashData
    {
        public Guid UserId { get; set; }
        public int TotalWaterSavedGallons { get; set; }
    }
}