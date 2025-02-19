using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Data.Entities;

namespace OrderService.Data.Configurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(x => x.Id);  
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();  
            builder.Property(x => x.StatusId).IsRequired(); 
            builder.HasOne(x => x.Status)
                   .WithMany()
                   .HasForeignKey(x => x.StatusId)
                   .OnDelete(DeleteBehavior.Restrict);  

         
        }
    }

   

 
}
