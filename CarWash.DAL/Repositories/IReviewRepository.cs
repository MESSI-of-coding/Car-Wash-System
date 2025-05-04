using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash.Domain.Models;

namespace CarWash.DAL.Repositories
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review);
        Task<List<Review>> GetByWasherIdAsync(int washerId);
    }
}