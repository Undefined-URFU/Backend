using CosmeticsRecommendationSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsRecommendationSystem.Database;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
