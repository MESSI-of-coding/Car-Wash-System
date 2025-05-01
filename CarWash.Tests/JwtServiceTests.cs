using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarWash.BL.Services;
using System;
using Microsoft.Extensions.Configuration;

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
            _configurationMock.Setup(config => config["Jwt:Key"]).Returns("TestSecretKey");
            _configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("TestIssuer");
            _configurationMock.Setup(config => config["Jwt:Audience"]).Returns("TestAudience");

            _jwtService = new JwtService(_configurationMock.Object);
        }

        [TestMethod]
        public void GenerateToken_ShouldReturnValidToken()
        {
            // Arrange
            var userId = 1;

            // Act
            var token = _jwtService.GenerateToken(userId);

            // Assert
            Assert.IsNotNull(token);
            Assert.IsInstanceOfType(token, typeof(string));
        }

        [TestMethod]
        public void ValidateToken_ShouldReturnTrue_ForValidToken()
        {
            // Arrange
            var userId = 1;
            var token = _jwtService.GenerateToken(userId);

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