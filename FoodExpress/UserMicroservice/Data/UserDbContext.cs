using FoodExpress.UserMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodExpress.UserMicroservice.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
