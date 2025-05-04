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
        Task<WashRequest> GetWashRequestByIdAsync(int id);
        Task<List<WashRequest>> GetAllWashRequestsAsync();
        Task<bool> UpdateWashRequestStatusAsync(int id, string status);
        Task<bool> IsValidCustomerId(int customerId);
        Task<bool> IsValidCarId(int carId);
        Task<bool> IsValidPackageId(int packageId);
        Task<IEnumerable<WashRequestDto>> GetWashRequestsByUserIdAsync(int userId);
        Task<List<WashRequest>> GetFilteredWashRequestsAsync(DateTime startDate, DateTime endDate, int serviceType); // Filters wash requests by date range and service type
    }
}