using CosmeticsRecommendationSystem.Database.Models;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsRecommendationSystem.Database.Repositories;

public class ProductRepository(DatabaseContext database) : IProductRepository
{
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await database.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await database.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
}