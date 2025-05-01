using CarWash.DAL.Repositories;
using CarWash.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface ICarService
    {
        Task<Car> AddCarAsync(Car car, int userId); // Add a new car for a user
        Task<IEnumerable<Car>> GetCarsByUserIdAsync(int userId); // Get all cars for a specific user
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
        public async Task<Car> AddCarAsync(Car car, int userId)
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
        public async Task<IEnumerable<Car>> GetCarsByUserIdAsync(int userId)
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
    }
}