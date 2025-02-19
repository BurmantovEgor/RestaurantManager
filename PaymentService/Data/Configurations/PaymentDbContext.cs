using Microsoft.EntityFrameworkCore;
using PaymentService.Data.Entities;
using System.Collections.Generic;

namespace OrderService.Data.Configurations
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

        public DbSet<OrderEntity> Order { get; set; }
        public DbSet<StatusEntity> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StatusEntityConfiguration());
        }
       
        public void SeedStatuses()
        {
            if (!Status.Any())
            {
                Status.AddRange(
                    new StatusEntity { Id = "NotPaid", StatusName = "Не оплачено" },
                    new StatusEntity { Id = "Paid", StatusName = "Оплачено" }
                );

                SaveChanges();
            }
        }

    }
}
