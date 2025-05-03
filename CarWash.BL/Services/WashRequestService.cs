using CarWash.DAL.Repositories;
using CarWash.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash.DAL.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CarWash.BL.DTOs;
using CarWash.BL.Events; // Add this line

namespace CarWash.BL.Services
{
    public class WashRequestService : IWashRequestService
    {
        private readonly IWashRequestRepository _washRequestRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public WashRequestService(IWashRequestRepository washRequestRepository, AppDbContext dbContext, IMapper mapper, IEventBus eventBus)
        {
            _washRequestRepository = washRequestRepository;
            _dbContext = dbContext;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<WashRequest> CreateWashRequestAsync(WashRequest request)
        {
            var createdRequest = await _washRequestRepository.AddAsync(request);

            // Publish RequestCreated event
            await _eventBus.PublishAsync(new RequestCreated
            {
                UserId = request.CustomerId,
                RequestId = createdRequest.RequestId
            });

            return createdRequest;
        }

        public async Task<WashRequest> GetWashRequestByIdAsync(int id)
        {
            return await _washRequestRepository.GetByIdAsync(id);
        }

        public async Task<List<WashRequest>> GetAllWashRequestsAsync()
        {
            return await _washRequestRepository.GetAllAsync();
        }

        public async Task<bool> UpdateWashRequestStatusAsync(int id, string status)
        {
            var request = await _washRequestRepository.GetByIdAsync(id);
            if (request == null || !Enum.TryParse(status, true, out WashStatus newStatus))
            {
                return false;
            }

            request.Status = newStatus;
            await _washRequestRepository.UpdateAsync(request);

            // Publish events based on status
            if (newStatus == WashStatus.Accepted)
            {
                await _eventBus.PublishAsync(new RequestAccepted
                {
                    UserId = request.CustomerId,
                    RequestId = request.RequestId
                });
            }
            else if (newStatus == WashStatus.Accepted) // Replace Assigned with Accepted
            {
                await _eventBus.PublishAsync(new WashRequestAssigned
                {
                    UserId = request.CustomerId,
                    RequestId = request.RequestId
                });
            }

            return true;
        }

        public async Task<bool> IsValidCustomerId(int customerId)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserId == customerId);
        }

        public async Task<bool> IsValidCarId(int carId)
        {
            return await _dbContext.Cars.AnyAsync(c => c.CarId == carId);
        }

        public async Task<bool> IsValidPackageId(int packageId)
        {
            return await _dbContext.WashPackages.AnyAsync(p => p.PackageId == packageId);
        }

        private bool IsValidStatus(string status)
        {
            var validStatuses = new List<string> { "Pending", "Accepted", "InProgress", "Completed", "Cancelled" };
            return validStatuses.Contains(status);
        }

        public async Task<IEnumerable<WashRequestDto>> GetWashRequestsByUserIdAsync(int userId)
        {
            var washRequests = await _washRequestRepository.GetWashRequestsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<WashRequestDto>>(washRequests);
        }
    }
}