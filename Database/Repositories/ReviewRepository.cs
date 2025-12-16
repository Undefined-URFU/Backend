using CosmeticsRecommendationSystem.Database.Models;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;

namespace CosmeticsRecommendationSystem.Database.Repositories;

public class ReviewRepository(DatabaseContext database) : IReviewRepository
{
    public async Task CreateReviewAsync(Review review)
    {
        database.Reviews.Add(review);
        await database.SaveChangesAsync();
    }
}
