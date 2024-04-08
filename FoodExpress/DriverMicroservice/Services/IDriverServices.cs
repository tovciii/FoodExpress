using FoodExpress.DriverMicroservice.Models;
using FoodExpress.UserMicroservice.Models;

namespace FoodExpress.DriverMicroservice.Services
{
    public interface IDriverServices
    {
        Task<List<Driver>> GetAllDriversAsync();
        Task<Driver> GetDriverByIdAsync(int id);
        Task<Driver> AddDriverAsync(Driver driver);
        Task<Driver> UpdateDriverAsync(Driver driver);
        Task<bool> DeleteDriverAsync(int id);
    }
}
