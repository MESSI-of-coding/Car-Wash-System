using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarWash.DAL.Data;
using CarWash.Domain.Models; // Add namespace for WashStatus

namespace CarWash.BL.Services
{
    public class AssignmentService
    {
        private readonly AppDbContext _dbContext;
        private readonly WasherAssignmentService _washerAssignmentService;

        public AssignmentService(AppDbContext dbContext, WasherAssignmentService washerAssignmentService)
        {
            _dbContext = dbContext;
            _washerAssignmentService = washerAssignmentService;
        }

        public async Task AssignPendingRequestsAsync()
        {
            // Update the WashStatus comparison
            var pendingRequests = await _dbContext.WashRequests
                .Where(wr => wr.Status == WashStatus.Pending)
                .ToListAsync();

            foreach (var request in pendingRequests)
            {
                await _washerAssignmentService.AssignWasherAsync(request.RequestId); // ✅ Pass the ID
            }
        }
    }
}