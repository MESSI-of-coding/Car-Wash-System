using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarWash.BL.Services;
using CarWash.DAL.Repositories;
using CarWash.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWash.Tests
{
    [TestClass]
    public class CarServiceTests
    {
        private Mock<ICarRepository> _carRepositoryMock;
        private Mock<IUserService> _userServiceMock; // Add mock for IUserService
        private CarService _carService;

        [TestInitialize]
        public void Setup()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _userServiceMock = new Mock<IUserService>();
            _carService = new CarService(
                _carRepositoryMock.Object,
                _userServiceMock.Object
            );
        }

        [TestMethod]
        public async Task GetAllCars_ShouldReturnListOfCars()
        {
            // Arrange
            var expectedCars = new List<Car>
            {
                new Car { CarId = 1, Model = "Model X", LicensePlate = "ABC123" }, // Add LicensePlate
                new Car { CarId = 2, Model = "Model Y", LicensePlate = "XYZ789" }
            };
            _carRepositoryMock.Setup(repo => repo.GetAllCarsAsync()).ReturnsAsync(expectedCars);

            // Act
            var result = await _carService.GetAllCarsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCars.Count, result.Count);
            _carRepositoryMock.Verify(repo => repo.GetAllCarsAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetCarById_ShouldReturnCar_WhenCarExists()
        {
            // Arrange
            var carId = 1;
            var expectedCar = new Car
            {
                CarId = carId,
                Model = "Model X",
                LicensePlate = "ABC123" // Add required property
            };
            _carRepositoryMock.Setup(repo => repo.GetCarByIdAsync(carId)).ReturnsAsync(expectedCar);

            // Act
            var result = await _carService.GetCarByIdAsync(carId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCar.Model, result.Model);
            _carRepositoryMock.Verify(repo => repo.GetCarByIdAsync(carId), Times.Once);
        }

        [TestMethod]
        public async Task GetCarById_ShouldReturnNull_WhenCarDoesNotExist()
        {
            // Arrange
            var carId = 1;
            _carRepositoryMock.Setup(repo => repo.GetCarByIdAsync(carId)).ReturnsAsync((Car)null);

            // Act
            var result = await _carService.GetCarByIdAsync(carId);

            // Assert
            Assert.IsNull(result);
            _carRepositoryMock.Verify(repo => repo.GetCarByIdAsync(carId), Times.Once);
        }

        [TestMethod]
        public void InitializeCarProperties()
        {
            // Arrange
            var car = new Car
            {
                LicensePlate = "ABC123", // Set required property
                Model = "Sedan"
            };

            // Assert
            Assert.IsNotNull(car);
            Assert.AreEqual("ABC123", car.LicensePlate);
            Assert.AreEqual("Sedan", car.Model);
        }
    }
}