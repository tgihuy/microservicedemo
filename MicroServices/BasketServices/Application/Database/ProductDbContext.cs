using BasketServices.Application.Entities;
using Microservices.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Application.Database
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CustomerBasket> CustomerBaskets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BasketItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerBasketEntityConfiguration());
        }
    }
}
