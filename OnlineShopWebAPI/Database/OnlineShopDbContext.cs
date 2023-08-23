using Microsoft.EntityFrameworkCore;
using OnlineShopWebAPI.Model;
using System.Reflection;

namespace OnlineShopWebAPI.Database
{
    public class OnlineShopDbContext : DbContext
    {
        public DbSet<User> UserAccounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public OnlineShopDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Order>().HasMany(x => x.OrderedProducts).WithMany(x => x.Orders);
        }
    }
}
