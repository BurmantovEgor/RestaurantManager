using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Data.Entities;

namespace OrderService.Data.Configurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(x => x.Id);  // Primary Key

            builder.Property(x => x.UserId).IsRequired();  // UserId обязательно

            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();  // Цена

            builder.Property(x => x.StatusId).IsRequired();  // Статус обязательно

            builder.HasOne(x => x.Status)
                   .WithMany()
                   .HasForeignKey(x => x.StatusId)
                   .OnDelete(DeleteBehavior.Restrict);  

            builder.HasMany(x => x.OrderDetails)
                   .WithOne()
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);  
        }
    }

   

 
}
