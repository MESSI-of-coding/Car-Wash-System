using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarWash.BL.Services;
using System;
using Microsoft.Extensions.Configuration;
using CarWash.Domain.Models;

namespace CarWash.Tests
{
    [TestClass]
    public class JwtServiceTests
    {
        private Mock<IConfiguration> _configurationMock;
        private JwtService _jwtService;

        [TestInitialize]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _jwtService = new JwtService(_configurationMock.Object);
        }

        [TestMethod]
        public void GenerateToken_ShouldReturnValidToken()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                Email = "test@example.com",
                PasswordHash = "hashed_password",
                Location = new NetTopologySuite.Geometries.Point(0, 0) { SRID = 4326 } // For DTO: map to { Latitude = Location.Y, Longitude = Location.X }
            };

            // Act
            var token = _jwtService.GenerateToken(user); // Pass User object

            // Assert
            Assert.IsNotNull(token);
            Assert.IsInstanceOfType(token, typeof(string));
        }

        [TestMethod]
        public void ValidateToken_ShouldReturnTrue_ForValidToken()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                Email = "test@example.com",
                PasswordHash = "hashed_password",
                Location = new NetTopologySuite.Geometries.Point(0, 0) { SRID = 4326 } // For DTO: map to { Latitude = Location.Y, Longitude = Location.X }
            };
            var token = _jwtService.GenerateToken(user);

            // Act
            var isValid = _jwtService.ValidateToken(token);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateToken_ShouldReturnFalse_ForInvalidToken()
        {
            // Arrange
            var invalidToken = "InvalidToken";

            // Act
            var isValid = _jwtService.ValidateToken(invalidToken);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}