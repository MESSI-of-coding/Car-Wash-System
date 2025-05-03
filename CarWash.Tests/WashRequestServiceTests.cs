using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarWash.BL.Services;
using CarWash.DAL.Repositories;
using CarWash.Domain.Models;
using CarWash.DAL.Data; // Add reference for AppDbContext
using System;
using System.Threading.Tasks;

namespace CarWash.Tests
{
    [TestClass]
    public class WashRequestServiceTests
    {
        private Mock<IWashRequestRepository>? _mockRepository;
        private Mock<AppDbContext>? _mockDbContext; // Add mock for AppDbContext
        private WashRequestService? _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IWashRequestRepository>();
            _mockDbContext = new Mock<AppDbContext>(); // Add mock for AppDbContext
            _service = new WashRequestService(_mockRepository.Object, _mockDbContext.Object); // Pass mock to constructor
        }

        [TestMethod]
        public async Task CreateWashRequestAsync_ShouldRejectOverlappingSchedules()
        {
            // Arrange
            if (_mockRepository == null || _service == null)
                Assert.Fail("Mocks not initialized");

            var carId = 1;
            var scheduledDateTime = new DateTime(2025, 5, 3, 10, 0, 0);

            // Simulate an existing wash request
            _mockRepository!
                .Setup(repo => repo.HasOverlappingScheduleAsync(carId, scheduledDateTime))
                .ReturnsAsync(true);

            var newRequest = new WashRequest
            {
                CarId = carId,
                ScheduledDateTime = scheduledDateTime
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
            {
                await _service!.CreateWashRequestAsync(newRequest);
            });

            _mockRepository.Verify(repo => repo.HasOverlappingScheduleAsync(carId, scheduledDateTime), Times.Once);
        }
    }
}