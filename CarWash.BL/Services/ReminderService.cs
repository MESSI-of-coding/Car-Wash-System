using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarWash.DAL.Data;
using CarWash.BL.Services; // Add this line

namespace CarWash.BL.Services
{
    public class ReminderService
    {
        private readonly AppDbContext _dbContext;
        private readonly NotificationService _notificationService;

        public ReminderService(AppDbContext dbContext, NotificationService notificationService)
        {
            _dbContext = dbContext;
            _notificationService = notificationService;
        }

        public async Task SendUpcomingRemindersAsync()
        {
            var now = DateTime.UtcNow;
            var windowStart = now.AddHours(2);
            var windowEnd = windowStart.AddMinutes(30); // Replace 30 with your desired minutesWindow

            var upcomingWashRequests = await _dbContext.WashRequests
                .Where(wr => wr.ScheduledDateTime >= windowStart && wr.ScheduledDateTime <= windowEnd)
                .ToListAsync();

            foreach (var washRequest in upcomingWashRequests)
            {
                await _notificationService.SendReminderAsync(washRequest.CustomerId, washRequest.RequestId); // Pass both userId and requestId
            }
        }
    }
}