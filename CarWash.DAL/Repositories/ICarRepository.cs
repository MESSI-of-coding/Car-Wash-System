using CarWash.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWash.DAL.Repositories
{
    public interface ICarRepository
    {
        Task<Car> AddCarAsync(Car car); // Add a new car
        Task<Car?> GetCarByIdAsync(int carId); // Get a car by its ID
        Task<IEnumerable<Car>> GetAllCarsAsync(); // Get all cars
        Task<Car> UpdateCarAsync(Car car); // Update an existing car
        Task<bool> DeleteCarAsync(int carId); // Delete a car by its ID
    }
}