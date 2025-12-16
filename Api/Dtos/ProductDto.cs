namespace CosmeticsRecommendationSystem.Api.Dtos;

public record ProductDto(
    string Id,
    string ProductId,
    string ProductName,
    string BrandName,
    int LovesCount,
    decimal Rating,
    string Size,
    string[] Ingredients,
    decimal Price,
    string[] Highlights,
    string PrimaryCategory
);