using FoodExpress.MenuMicroservice.DTO;
using FoodExpress.MenuMicroservice.Models;
using FoodExpress.RelationData;
using Microsoft.EntityFrameworkCore;

namespace FoodExpress.MenuMicroservice.Services
{
    public class MenuServices : IMenuServices
    {
        private readonly DataContext _context;

        public MenuServices(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuDTO>> GetAllMenusAsync()
        {
            var menus = await _context.Menus.ToListAsync();
            return menus.Select(menu => new MenuDTO
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category,
                Price = menu.Price,

                RestaurantId = menu.RestaurantId
            });
        }

        public async Task<MenuDTO> GetMenuByIdAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
                return null;

            return new MenuDTO
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category,
                Price = menu.Price,

                RestaurantId = menu.RestaurantId
            };
        }

        public async Task<MenuDTO> CreateMenuAsync(int restaurantId, MenuDTO menuDTO)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantId);

            if (restaurant == null)
            {
                throw new Exception("Restaurant not found");
            }

            var newMenu = new Menu
            {
                Name = menuDTO.Name,
                Description = menuDTO.Description,
                Category = menuDTO.Category,
                Price = menuDTO.Price,

                RestaurantId = restaurantId
            };

            _context.Menus.Add(newMenu);
            await _context.SaveChangesAsync();

            menuDTO.MenuId = newMenu.MenuId;
            return menuDTO;
        }

        public async Task<MenuDTO> UpdateMenuAsync(int id, MenuDTO menuDTO)
        {
            var existingMenu = await _context.Menus.FindAsync(id);

            if (existingMenu == null)
            {
                throw new Exception("Menu not found");
            }

            existingMenu.Name = menuDTO.Name;
            existingMenu.Description = menuDTO.Description;
            existingMenu.Category = menuDTO.Category;
            existingMenu.Price = menuDTO.Price;

            await _context.SaveChangesAsync();

            return menuDTO;
        }

        public async Task DeleteMenuAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
                throw new Exception("Menu not found");

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
    }
}

