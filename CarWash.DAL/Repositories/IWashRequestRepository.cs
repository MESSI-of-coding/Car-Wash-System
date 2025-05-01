using CarWash.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWash.DAL.Repositories
{
    public interface IWashRequestRepository
    {
        Task<WashRequest> AddAsync(WashRequest request);
        Task<WashRequest> GetByIdAsync(int id);
        Task<List<WashRequest>> GetAllAsync();
        Task UpdateAsync(WashRequest request);
    }
}