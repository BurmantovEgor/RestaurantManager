using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;
using System.Collections.Generic;

namespace OrderService.Data.Configurations
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<OrderEntity> Order { get; set; }
        public DbSet<StatusEntity> Status { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StatusEntityConfiguration());
        }
       
        public void SeedStatuses()
        {
            if (!Status.Any())
            {
                Status.AddRange(
                    new StatusEntity { Id = new Guid("ff5d8e82-ddce-487b-8c18-3150f1d459a4"), Name = "Новое" },
                    new StatusEntity { Id = Guid.NewGuid(), Name = "В работе" },
                    new StatusEntity { Id = Guid.NewGuid(), Name = "Выполнено" }
                );

                SaveChanges();
            }
        }

    }
}
