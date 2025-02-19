using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KitchenService.Data;

public class OrderDetailEntityConfiguration : IEntityTypeConfiguration<OrderDetailEntity>
{
    public void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
    {
        builder.HasKey(x => x.Id); 

        builder.Property(x => x.ProductId).IsRequired();  

        builder.Property(x => x.ProductCount).IsRequired();  

    }
}