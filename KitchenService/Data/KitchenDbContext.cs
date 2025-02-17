using KitchenService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KitchenService.Data;

public class KitchenDbContext : DbContext
{
    public KitchenDbContext(DbContextOptions<KitchenDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
}
