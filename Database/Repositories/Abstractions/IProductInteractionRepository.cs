using CosmeticsRecommendationSystem.Database.Models;

namespace CosmeticsRecommendationSystem.Database.Repositories.Abstractions;

public interface IProductInteractionRepository
{
    Task CreateInteractionAsync(ProductInteraction interaction);
    Task<ProductInteraction?> GetInteractionByUserAndProductAsync(Guid userId, Guid productId);
}
