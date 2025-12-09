namespace CosmeticsRecommendationSystem.Api.Abstractions;

public interface IJwtService
{
    public string GenerateToken(string userId);
}
