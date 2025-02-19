using KitchenService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Data.Configurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(x => x.Id);  
            builder.Property(x => x.StatusId).IsRequired();  
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
