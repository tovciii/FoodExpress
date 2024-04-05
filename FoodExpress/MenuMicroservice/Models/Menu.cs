using FoodExpress.RestaurantMicroservice.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodExpress.MenuMicroservice.Models
{
    [Table("menus")]
    public class Menu
    {
        [Column("menu_id")]
        public int MenuId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("restaurant_id")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
