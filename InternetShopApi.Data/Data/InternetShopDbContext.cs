
using InternetShopApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace InternetShopApi.Data.Data
{
    public class InternetShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public InternetShopDbContext(DbContextOptions<InternetShopDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> Items { get; set; }

    }
}
