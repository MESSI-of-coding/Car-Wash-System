using CarWash.Domain.Models;
using CarWash.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWash.DAL.Repositories
{
    public class WashRequestRepository : IWashRequestRepository
    {
        private readonly AppDbContext _context;

        public WashRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WashRequest> AddAsync(WashRequest request)
        {
            await _context.WashRequests.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<WashRequest> GetByIdAsync(int id)
        {
            return await _context.WashRequests.FindAsync(id);
        }

        public async Task<List<WashRequest>> GetAllAsync()
        {
            return await _context.WashRequests.ToListAsync();
        }

        public async Task UpdateAsync(WashRequest request)
        {
            _context.WashRequests.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}