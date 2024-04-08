using FoodExpress.DriverMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodExpress.DriverMicroservice.Data
{
    public class DriverDbContext : DbContext
    {
        public DriverDbContext(DbContextOptions<DriverDbContext> options) : base(options) 
        { 
        }

        public DbSet<Driver> Drivers { get; set; }
    }
}
