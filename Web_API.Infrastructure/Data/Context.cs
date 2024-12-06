using Microsoft.EntityFrameworkCore;
using Web_API.Domain.Items;
using Web_API.Domain.Order;

namespace Web_API.Infrastructure.Data
{
    public class Context : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
