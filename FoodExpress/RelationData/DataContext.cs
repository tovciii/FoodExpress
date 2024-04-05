using FoodExpress.MenuMicroservice.Models;
using FoodExpress.RestaurantMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodExpress.RelationData
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Menus)
                .WithOne(m => m.Restaurant)
                .HasForeignKey(m => m.RestaurantId);
        }
    }
}
