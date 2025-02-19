using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;

public class StatusEntityConfiguration : IEntityTypeConfiguration<StatusEntity>
{
    public void Configure(EntityTypeBuilder<StatusEntity> builder)
    {
        builder.HasKey(x => x.Id);  

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100); 
    }
}