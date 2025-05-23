using Microsoft.AspNetCore.Identity; // Added for IPasswordHasher
using CarWash.DAL.Data; // Added for AppDbContext
using CarWash.DAL.Repositories; // Added for IUserRepository
using CarWash.BL.DTOs;
using CarWash.Domain.Models;
using Microsoft.EntityFrameworkCore; // Added for EF Core async methods

namespace CarWash.BL.Services
{
    public interface IUserService
    {
        Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto registerDto);
        Task<User?> ValidateUserAsync(LoginDto loginDto);
        Task<User?> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync(); // Retrieves all users
        Task<bool> UpdateUserStatusAsync(int userId, bool isActive); // Updates the activation status of a user
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto registerDto)
        {
            if (await _userRepository.AnyAsync(u => u.Email == registerDto.Email))
            {
                return (false, "Email is already registered.");
            }

            // Fix for User.Location required member
            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(new User { Email = "temp", PasswordHash = "temp", Location = new NetTopologySuite.Geometries.Point(0, 0) { SRID = 4326 } }, registerDto.Password),
                FullName = registerDto.FullName,
                ContactNumber = registerDto.ContactNumber,
                IsActive = true,
                Location = new NetTopologySuite.Geometries.Point(registerDto.Location.Longitude, registerDto.Location.Latitude) { SRID = 4326 } // Set from DTO
            };

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return (true, "User registered successfully.");
        }

        public async Task<User?> ValidateUserAsync(LoginDto loginDto)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return null; // Ensure the method's return type allows null
            }

            if (!_passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginDto.Password).Equals(PasswordVerificationResult.Success))
            {
                return null;
            }

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.FindAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> UpdateUserStatusAsync(int userId, bool isActive)
        {
            var user = await _userRepository.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = isActive;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}