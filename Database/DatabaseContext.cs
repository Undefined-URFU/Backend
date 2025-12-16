using CosmeticsRecommendationSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsRecommendationSystem.Database;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserPreferences> UsersPreferences { get; set; }
    public DbSet<ProductInteraction> ProductInteractions { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
