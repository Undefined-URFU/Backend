using CosmeticsRecommendationSystem.Database.Models;

namespace CosmeticsRecommendationSystem.Database.Repositories.Abstractions;

public interface IReviewRepository
{
    Task CreateReviewAsync(Review review);
}