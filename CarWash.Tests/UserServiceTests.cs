using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarWash.BL.Services;
using CarWash.DAL.Repositories;
using CarWash.Domain.Models;
using System.Threading.Tasks;

namespace CarWash.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new User { UserId = userId, Email = "test@example.com" };
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
    }
}