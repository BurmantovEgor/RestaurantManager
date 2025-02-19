using MenuService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuService.Data.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<DishEntity>
    {
        public void Configure(EntityTypeBuilder<DishEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(120);
        }
    }
}
