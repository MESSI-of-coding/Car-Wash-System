using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWash.DAL.Data;
using CarWash.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWash.DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetByWasherIdAsync(Guid washerId)
        {
            return await _context.Reviews
                .Where(r => r.WasherId == washerId) // Ensure WasherId is used
                .ToListAsync();
        }
    }
}