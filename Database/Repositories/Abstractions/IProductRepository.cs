using CosmeticsRecommendationSystem.Database.Models;

namespace CosmeticsRecommendationSystem.Database.Repositories.Abstractions;

public interface IProductRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
}