using FoodExpress.MenuMicroservice.DTO;
using FoodExpress.MenuMicroservice.Models;
using FoodExpress.RelationData;
using FoodExpress.RestaurantMicroservice.DTO;
using FoodExpress.RestaurantMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodExpress.RestaurantMicroservice.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly DataContext _context;

        public RestaurantServices(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync()
        {
            var restaurants = await _context.Restaurants.Include(r => r.Menus).ToListAsync();
            return restaurants.Select(r => new RestaurantDTO
            {
                RestaurantId = r.RestaurantId,
                Name = r.Name,
                Address = r.Address,
                City = r.City,
                PhoneNumber = r.PhoneNumber,

                Menus = r.Menus.Select(m => new MenuDTO
                {
                    MenuId = m.MenuId,
                    Name = m.Name,
                    Description = m.Description,
                    Category = m.Category,
                    Price = m.Price
                }).ToList()
            });
        }

        public async Task<RestaurantDTO> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants.Include(r => r.Menus).FirstOrDefaultAsync(r => r.RestaurantId == id);
            if (restaurant == null)
                return null;

            return new RestaurantDTO
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                Address = restaurant.Address,
                City = restaurant.City,
                PhoneNumber = restaurant.PhoneNumber,

                Menus = restaurant.Menus.Select(m => new MenuDTO
                {
                    MenuId = m.MenuId,
                    Name = m.Name,
                    Description = m.Description,
                    Category = m.Category,
                    Price = m.Price
                }).ToList()
            };
        }

        public async Task<RestaurantDTO> CreateRestaurantAsync(RestaurantDTO restaurantDTO)
        {
            var existingRestaurant = await _context.Restaurants.Include(r => r.Menus).FirstOrDefaultAsync(r => r.Name == restaurantDTO.Name);
            if (existingRestaurant != null)
            {
                if (restaurantDTO.Menus != null)
                {
                    foreach (var menuDTO in restaurantDTO.Menus)
                    {
                        existingRestaurant.Menus.Add(new Menu
                        {
                            Name = menuDTO.Name,
                            Description = menuDTO.Description,
                            Category = menuDTO.Category,
                            Price = menuDTO.Price
                        });
                    }
                }

                await _context.SaveChangesAsync();

                var existingRestaurantDTO = new RestaurantDTO
                {
                    RestaurantId = existingRestaurant.RestaurantId,
                    Name = existingRestaurant.Name,
                    Address = existingRestaurant.Address,
                    City = existingRestaurant.City,
                    PhoneNumber = existingRestaurant.PhoneNumber,

                    Menus = existingRestaurant.Menus.Select(m => new MenuDTO
                    {
                        MenuId = m.MenuId,
                        Name = m.Name,
                        Description = m.Description,
                        Category = m.Category,
                        Price = m.Price
                    }).ToList()
                };

                return existingRestaurantDTO;
            }
            else
            {
                var newRestaurant = new Restaurant
                {
                    Name = restaurantDTO.Name,
                    Address = restaurantDTO.Address,
                    City = restaurantDTO.City,
                    PhoneNumber = restaurantDTO.PhoneNumber,

                    Menus = restaurantDTO.Menus?.Select(m => new Menu
                    {
                        Name = m.Name,
                        Description = m.Description,
                        Category = m.Category,
                        Price = m.Price
                    }).ToList()
                };

                _context.Restaurants.Add(newRestaurant);
                await _context.SaveChangesAsync();

                var newRestaurantDTO = new RestaurantDTO
                {
                    RestaurantId = newRestaurant.RestaurantId,
                    Name = newRestaurant.Name,
                    Address = newRestaurant.Address,
                    City = newRestaurant.City,
                    PhoneNumber = newRestaurant.PhoneNumber,

                    Menus = newRestaurant.Menus?.Select(m => new MenuDTO
                    {
                        MenuId = m.MenuId,
                        Name = m.Name,
                        Description = m.Description,
                        Category = m.Category,
                        Price = m.Price
                    }).ToList()
                };

                return newRestaurantDTO;
            }
        }

        public async Task<RestaurantDTO> UpdateRestaurantAsync(int id, RestaurantDTO restaurantDTO)
        {
            var existingRestaurant = await _context.Restaurants
                .Include(r => r.Menus)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (existingRestaurant == null)
            {
                throw new Exception("Restaurant not found");
            }

            existingRestaurant.Name = restaurantDTO.Name;
            existingRestaurant.Address = restaurantDTO.Address;
            existingRestaurant.City = restaurantDTO.City;
            existingRestaurant.PhoneNumber = restaurantDTO.PhoneNumber;

            if (restaurantDTO.Menus != null)
            {
                if (existingRestaurant.Menus == null)
                {
                    existingRestaurant.Menus = new List<Menu>();
                }

                foreach (var menuDTO in restaurantDTO.Menus)
                {
                    if (menuDTO.MenuId == 0)
                    {
                        existingRestaurant.Menus.Add(new Menu
                        {
                            Name = menuDTO.Name,
                            Description = menuDTO.Description,
                            Category = menuDTO.Category,
                            Price = menuDTO.Price
                        });
                    }
                    else
                    {
                        var existingMenu = existingRestaurant.Menus.FirstOrDefault(m => m.MenuId == menuDTO.MenuId);
                        if (existingMenu != null)
                        {
                            existingMenu.Name = menuDTO.Name;
                            existingMenu.Description = menuDTO.Description;
                            existingMenu.Category = menuDTO.Category;
                            existingMenu.Price = menuDTO.Price;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            var updatedRestaurantDTO = new RestaurantDTO
            {
                RestaurantId = existingRestaurant.RestaurantId,
                Name = existingRestaurant.Name,
                Address = existingRestaurant.Address,
                City = existingRestaurant.City,
                PhoneNumber = existingRestaurant.PhoneNumber,
                Menus = existingRestaurant.Menus?.Select(m => new MenuDTO
                {
                    MenuId = m.MenuId,
                    Name = m.Name,
                    Description = m.Description,
                    Category = m.Category,
                    Price = m.Price
                }).ToList()
            };

            return updatedRestaurantDTO;
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                throw new Exception("Restaurant not found");
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}

