using FoodExpress.MenuMicroservice.DTO;

namespace FoodExpress.RestaurantMicroservice.DTO
{
    public class RestaurantDTO
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

        public List<MenuDTO> Menus { get; set; }
    }
}
