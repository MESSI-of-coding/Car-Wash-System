using CarWash.DAL.Repositories;
using CarWash.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface ICarService
    {
        Task<Car> AddCarAsync(Car car, Guid userId); // Add a new car for a user
        Task<IEnumerable<Car>> GetCarsByUserIdAsync(Guid userId); // Get all cars for a specific user
        Task<List<Car>> GetAllCarsAsync(); // Get all cars
        Task<Car?> GetCarByIdAsync(Guid carId); // Get a car by its ID
    }

    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserService _userService;
        private const int MaxCarsPerUser = 5; // Maximum number of cars a user can have

        public CarService(ICarRepository carRepository, IUserService userService)
        {
            _carRepository = carRepository;
            _userService = userService;
        }

        /// <summary>
        /// Adds a new car for a user, ensuring the user does not exceed the maximum allowed cars.
        /// </summary>
        /// <param name="car">The car to add.</param>
        /// <param name="userId">The ID of the user adding the car.</param>
        /// <returns>The added car.</returns>
        public async Task<Car> AddCarAsync(Car car, Guid userId)
        {
            var userCars = await _carRepository.GetAllCarsAsync();
            var userCarCount = 0;

            foreach (var userCar in userCars)
            {
                if (userCar.UserId == userId)
                {
                    userCarCount++;
                }
            }

            if (userCarCount >= MaxCarsPerUser)
            {
                throw new InvalidOperationException($"User with ID {userId} cannot have more than {MaxCarsPerUser} cars.");
            }

            car.UserId = userId;
            return await _carRepository.AddCarAsync(car);
        }

        /// <summary>
        /// Retrieves all cars associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose cars are to be retrieved.</param>
        /// <returns>A list of cars associated with the user.</returns>
        public async Task<IEnumerable<Car>> GetCarsByUserIdAsync(Guid userId)
        {
            var allCars = await _carRepository.GetAllCarsAsync();
            var userCars = new List<Car>();

            foreach (var car in allCars)
            {
                if (car.UserId == userId)
                {
                    userCars.Add(car);
                }
            }

            return userCars;
        }

        /// <summary>
        /// Retrieves all cars.
        /// </summary>
        /// <returns>A list of all cars.</returns>
        public async Task<List<Car>> GetAllCarsAsync()
        {
            return (await _carRepository.GetAllCarsAsync()).ToList();
        }

        /// <summary>
        /// Retrieves a car by its ID.
        /// </summary>
        /// <param name="carId">The ID of the car to retrieve.</param>
        /// <returns>The car with the specified ID, or null if not found.</returns>
        public async Task<Car?> GetCarByIdAsync(Guid carId)
        {
            return await _carRepository.GetCarByIdAsync(carId);
        }
    }
}