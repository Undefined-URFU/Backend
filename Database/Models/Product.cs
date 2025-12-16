namespace CosmeticsRecommendationSystem.Database.Models;

public class Product
{
    public Guid Id { get; set; }
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string BrandName { get; set; }
    public required int LovesCount { get; set; }
    public required decimal Rating { get; set; }
    public required string Size { get; set; }
    public required string[] Ingredients { get; set; }
    public required decimal Price { get; set; }
    public required string[] Highlights{ get; set; }
    public required string PrimaryCategory { get; set; }
}
