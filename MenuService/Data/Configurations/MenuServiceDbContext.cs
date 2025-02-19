using MenuService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuService.Data.Configurations
{
    public class MenuServiceDbContext : DbContext
    {
        public MenuServiceDbContext(DbContextOptions<MenuServiceDbContext> options) : base(options) { }

        public DbSet<DishEntity> Dish { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DishConfiguration());
        }

    }
}
