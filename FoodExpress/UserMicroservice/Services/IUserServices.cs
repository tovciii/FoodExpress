using FoodExpress.UserMicroservice.Models;

namespace FoodExpress.UserMicroservice.Services
{
    public interface IUserServices
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
