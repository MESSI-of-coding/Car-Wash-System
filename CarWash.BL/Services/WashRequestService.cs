using CarWash.DAL.Repositories;
using CarWash.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.BL.Services
{
    public class WashRequestService : IWashRequestService
    {
        private readonly IWashRequestRepository _washRequestRepository;
        private readonly AppDbContext _dbContext;

        public WashRequestService(IWashRequestRepository washRequestRepository, AppDbContext dbContext)
        {
            _washRequestRepository = washRequestRepository;
            _dbContext = dbContext;
        }

        public async Task<WashRequest> CreateWashRequestAsync(WashRequest request)
        {
            return await _washRequestRepository.AddAsync(request);
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
    }
}