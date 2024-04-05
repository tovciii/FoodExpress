namespace FoodExpress.MenuMicroservice.DTO
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }

        public int RestaurantId { get; set; }
    }
}
