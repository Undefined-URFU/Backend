using CosmeticsRecommendationSystem.Database.Models;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsRecommendationSystem.Database.Repositories;

public class ProductInteractionRepository(DatabaseContext database) : IProductInteractionRepository
{
    public async Task CreateInteractionAsync(ProductInteraction interaction)
    {
        database.ProductInteractions.Add(interaction);
        await database.SaveChangesAsync();
    }

    public async Task<ProductInteraction?> GetInteractionByUserAndProductAsync(Guid userId, Guid productId)
    {
        return await database.ProductInteractions.FirstOrDefaultAsync(pi => pi.UserId == userId && pi.ProductId == productId);
    }
}