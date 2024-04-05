using FoodExpress.RestaurantMicroservice.DTO;

namespace FoodExpress.RestaurantMicroservice.Services
{
    public interface IRestaurantServices
    {
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync();
        Task<RestaurantDTO> GetRestaurantByIdAsync(int id);
        Task<RestaurantDTO> CreateRestaurantAsync(RestaurantDTO restaurantDTO);
        Task<RestaurantDTO> UpdateRestaurantAsync(int id, RestaurantDTO restaurantDTO);
        Task DeleteRestaurantAsync(int id);
    }
}
