using Cafeinated.Backend.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = Cafeinated.Backend.Core.Entities.File;

namespace Cafeinated.Backend.Core.Database;

public class AppDBContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<CoffeeShop> CoffeeShops { get; set; }
    public DbSet<CoffeeType> CoffeeTypes { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderCoffeeType> OrderCoffeeTypes { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        
    }
}