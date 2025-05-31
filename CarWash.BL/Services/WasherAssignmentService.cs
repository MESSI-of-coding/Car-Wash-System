using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarWash.DAL.Data;
using CarWash.Domain.Models;
using NetTopologySuite.Geometries; // Add this using directive

namespace CarWash.BL.Services
{
    public class WasherAssignmentService : IWasherAssignmentService
    {
        private readonly AppDbContext _dbContext;
        private readonly IEventPublisher _eventPublisher;

        public WasherAssignmentService(AppDbContext dbContext, IEventPublisher eventPublisher)
        {
            _dbContext = dbContext;
            _eventPublisher = eventPublisher;
        }

        public async Task AssignWasherAsync(Guid requestId)
        {
            // Fetch the wash request
            var washRequest = await _dbContext.WashRequests
                .Include(wr => wr.Customer) // Include the Customer
                .FirstOrDefaultAsync(wr => wr.RequestId == requestId);

            if (washRequest == null || washRequest.Status != WashStatus.Pending)
            {
                throw new InvalidOperationException("Invalid wash request or status.");
            }

            // Use the customer’s location from Users table
            var customerLocation = washRequest.Customer.Location;
            // If you need to use DTOs elsewhere, ensure you map:
            // var locationDto = new LocationDto { Latitude = customerLocation.Y, Longitude = customerLocation.X };

            // Add null check for Customer
            if (washRequest.Customer == null || washRequest.Customer.Location == null)
            {
                throw new InvalidOperationException("Customer or location is invalid.");
            }

            // Fetch available washers (users with the "Washer" role)
            var availableWashers = await _dbContext.Users
                .Where(u => u.IsActive &&
                    u.UserRoles.Any(ur => ur.Role.RoleName == "Washer"))
                .ToListAsync();

            if (!availableWashers.Any())
            {
                throw new InvalidOperationException("No available washers.");
            }

            // Calculate the nearest washer
            var nearestWasher = availableWashers
                .OrderBy(w => CalculateDistance(customerLocation, w.Location))
                .FirstOrDefault();

            if (nearestWasher == null)
            {
                throw new InvalidOperationException("No suitable washer found.");
            }

            // Update the WashRequest record
            washRequest.WasherId = nearestWasher.UserId; // ✅ Use UserId
            washRequest.Status = WashStatus.Accepted; // ✅ Matches the enum
            await _dbContext.SaveChangesAsync();

            // Publish a WashRequestAssigned event
            await _eventPublisher.PublishAsync(new WashRequestAssignedEvent
            {
                RequestId = washRequest.RequestId,
                WasherId = nearestWasher.UserId // ✅ Use UserId
            });
        }

        private double CalculateDistance(Point location1, Point location2)
        {
            // Use X (longitude) and Y (latitude) from Point
            return Math.Sqrt(
                Math.Pow(location1.Y - location2.Y, 2) + // Latitude
                Math.Pow(location1.X - location2.X, 2)   // Longitude
            );
        }
    }
}