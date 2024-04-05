using FoodExpress.MenuMicroservice.DTO;

namespace FoodExpress.MenuMicroservice.Services
{
    public interface IMenuServices
    {
        Task<IEnumerable<MenuDTO>> GetAllMenusAsync();
        Task<MenuDTO> GetMenuByIdAsync(int id);
        Task<MenuDTO> CreateMenuAsync(int restaurantId, MenuDTO menuDTO);
        Task<MenuDTO> UpdateMenuAsync(int id, MenuDTO menuDTO);
        Task DeleteMenuAsync(int id);
    }
}
