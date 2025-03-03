using Microsoft.EntityFrameworkCore;
using OrderService.Core.Entities;

namespace OrderService.Infrastructure.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            Database.EnsureCreated();   
        }

        public DbSet<OrderEntity> Orders { get; set; }
    }
}
