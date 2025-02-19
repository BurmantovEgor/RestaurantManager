using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data.Entities;

public class StatusEntityConfiguration : IEntityTypeConfiguration<StatusEntity>
{
    public void Configure(EntityTypeBuilder<StatusEntity> builder)
    {
        builder.HasKey(x => x.Id);  

        builder.Property(x => x.StatusName).IsRequired().HasMaxLength(100); 
    }
}