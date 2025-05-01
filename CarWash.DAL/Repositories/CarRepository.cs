using CarWash.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash.DAL.Data;

namespace CarWash.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _dbContext;

        public CarRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            _dbContext.Cars.Add(car);
            await _dbContext.SaveChangesAsync();
            return car;
        }

        public async Task<Car?> GetCarByIdAsync(int carId)
        {
            return await _dbContext.Cars.FindAsync(carId);
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _dbContext.Cars.ToListAsync();
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            _dbContext.Cars.Update(car);
            await _dbContext.SaveChangesAsync();
            return car;
        }

        public async Task<bool> DeleteCarAsync(int carId)
        {
            var car = await _dbContext.Cars.FindAsync(carId);
            if (car == null)
            {
                return false;
            }

            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}