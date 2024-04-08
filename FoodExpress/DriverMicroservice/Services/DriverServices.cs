using FoodExpress.DriverMicroservice.Data;
using FoodExpress.DriverMicroservice.Models;
using FoodExpress.UserMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodExpress.DriverMicroservice.Services
{
    public class DriverServices : IDriverServices
    {
        private readonly DriverDbContext _context;

        public DriverServices(DriverDbContext context)
        {
            _context = context;
        }

        public async Task<List<Driver>> GetAllDriversAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task<Driver> AddDriverAsync(Driver driver)
        {
            driver.Password = BCrypt.Net.BCrypt.HashPassword(driver.Password);

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<Driver> UpdateDriverAsync(Driver driver)
        {
            if (!string.IsNullOrEmpty(driver.Password))
            {
                driver.Password = BCrypt.Net.BCrypt.HashPassword(driver.Password);
            }

            _context.Entry(driver).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
                return false;

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return true;
        }       
    }
}

