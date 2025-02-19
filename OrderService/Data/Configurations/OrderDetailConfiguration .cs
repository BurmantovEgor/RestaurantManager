using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;

public class OrderDetailEntityConfiguration : IEntityTypeConfiguration<OrderDetailEntity>
{
    public void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
    {
        builder.HasKey(x => x.Id);  // Primary Key

        builder.Property(x => x.ProductId).IsRequired();  // Идентификатор блюда

        builder.Property(x => x.ProductCount).IsRequired();  // Количество блюда

    }
}