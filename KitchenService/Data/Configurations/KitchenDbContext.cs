using Microsoft.EntityFrameworkCore;
using OrderService.Data.Configurations;
using System.Collections.Generic;

namespace KitchenService.Data.Configurations;

public class KitchenDbContext : DbContext
{
    public KitchenDbContext(DbContextOptions<KitchenDbContext> options) : base(options) { }

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
                new StatusEntity { Id = "WaitPay", Name = "Ожидает оплаты" },
                new StatusEntity { Id = "InProgress", Name = "Готовится" },
                new StatusEntity { Id = "Done", Name = "Готово" }

            );

            SaveChanges();
        }
    }
}
