using FoodExpress.MenuMicroservice.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodExpress.RestaurantMicroservice.Models
{
    [Table("restaurants")]
    public class Restaurant
    {
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }

        [Column("restaurant_name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        public List<Menu> Menus { get; set; }
    }
}
