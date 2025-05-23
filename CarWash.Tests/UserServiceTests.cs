using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarWash.BL.Services;
using CarWash.DAL.Repositories;
using CarWash.DAL.Data;
using CarWash.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity; // Add reference for IPasswordHasher

namespace CarWash.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IPasswordHasher<User>> _passwordHasherMock; // Add mock for IPasswordHasher<User>
        private Mock<IUserRepository> _userRepositoryMock; // Add mock for IUserRepository
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>(); // Use repository mock instead of DbContext
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();
            _userService = new UserService(
                _userRepositoryMock.Object, // Use repository mock
                _passwordHasherMock.Object
            );
        }

        [TestMethod]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new User
            {
                UserId = userId,
                Email = "test@example.com",
                PasswordHash = "temp_hash",
                Location = new NetTopologySuite.Geometries.Point(0, 0) { SRID = 4326 } // For DTO: map to { Latitude = Location.Y, Longitude = Location.X }
            };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Email, result.Email);
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once);
        }

        [TestMethod]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.IsNull(result);
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once);
        }

        [TestMethod]
        public void InitializeUserProperties()
        {
            // Arrange
            var user = new User
            {
                Email = "test@example.com",
                PasswordHash = "hashed_password", // Set required property
                Location = new NetTopologySuite.Geometries.Point(0, 0) { SRID = 4326 } // Set required property
            };

            // Assert
            Assert.AreEqual("hashed_password", user.PasswordHash);
            Assert.AreEqual(new NetTopologySuite.Geometries.Point(0, 0) { SRID = 4326 }, user.Location);
        }
    }
}