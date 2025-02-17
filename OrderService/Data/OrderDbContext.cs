using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;
using System.Collections.Generic;

namespace OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<OrderEntity> Orders { get; set; }
    }
}
