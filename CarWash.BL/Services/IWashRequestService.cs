using CarWash.BL.DTOs;
using CarWash.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface IWashRequestService
    {
        Task<WashRequest> CreateWashRequestAsync(WashRequest request);
        Task<WashRequest> GetWashRequestByIdAsync(Guid id);
        Task<List<WashRequest>> GetAllWashRequestsAsync();
        Task<bool> UpdateWashRequestStatusAsync(Guid id, string status);
        Task<bool> IsValidCustomerId(Guid customerId);
        Task<bool> IsValidCarId(Guid carId);
        Task<bool> IsValidPackageId(Guid packageId);
        Task<IEnumerable<WashRequestDto>> GetWashRequestsByUserIdAsync(Guid userId);
        Task<List<WashRequest>> GetFilteredWashRequestsAsync(DateTime startDate, DateTime endDate, Guid serviceType); // Filters wash requests by date range and service type
    }
}