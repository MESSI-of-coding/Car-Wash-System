using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash.Domain.Models;

namespace CarWash.BL.Services
{
    public interface IWasherService
    {
        Task<List<User>> GetAllWashersAsync(); // Retrieves all washers
        Task<bool> UpdateWasherAsync(Guid id, User washer); // Updates washer details
        Task<bool> SoftDeleteWasherAsync(Guid id); // Soft deletes a washer
    }
}