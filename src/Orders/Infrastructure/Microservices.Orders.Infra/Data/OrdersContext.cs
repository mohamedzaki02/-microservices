using Microservices.Orders.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Orders.Infra.Data
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
    }
}