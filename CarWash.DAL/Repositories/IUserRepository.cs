using CarWash.Domain.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarWash.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);
        void Add(User user);
        Task<int> SaveChangesAsync();
        Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate);
        Task<User?> FindAsync(Guid userId);
        Task<List<User>> GetAllAsync(); // Retrieves all users
        void Update(User user); // Updates an existing user
    }
}