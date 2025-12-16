using CosmeticsRecommendationSystem.Api.Dtos;
using CosmeticsRecommendationSystem.Database.Enums;
using CosmeticsRecommendationSystem.Database.Models;
using CosmeticsRecommendationSystem.Database.Repositories;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsRecommendationSystem.Api.Controllers;

[ApiController]
[Route("api/v1/products")]
[Authorize]
public class ProductsController(
    IUserRepository userRepository,
    IProductRepository productRepository,
    IProductInteractionRepository interactionRepository,
    IReviewRepository reviewRepository
) : ControllerBase
{
    [HttpGet]
    public async Task<List<ProductDto>> GetProducts()
    {
        var products = await productRepository.GetAllProductsAsync();
        return [.. products.Select(p => new ProductDto(
            p.Id.ToString(),
            p.ProductId,
            p.ProductName,
            p.BrandName,
            p.LovesCount,
            p.Rating,
            p.Size,
            p.Ingredients,
            p.Price,
            p.Highlights,
            p.PrimaryCategory
        )).Take(100)];
    }

    [HttpGet("recommended")]
    public async Task<List<ProductDto>> GetRecommendedProducts([FromQuery] int amount = 5)
    {
        // Заглушка: Возвращаем первые 'amount' продуктов из DB (пока без ML)
        var products = await productRepository.GetAllProductsAsync();
        var recommended = products.Take(amount).ToList();

        return [.. recommended.Select(p => new ProductDto(
            p.Id.ToString(),
            p.ProductId,
            p.ProductName,
            p.BrandName,
            p.LovesCount,
            p.Rating,
            p.Size,
            p.Ingredients,
            p.Price,
            p.Highlights,
            p.PrimaryCategory
        ))];
    }

    [HttpPost("like/{id}")]
    public async Task LikeProduct(string id)  // id - это Guid.ToString() продукта
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        var user = await userRepository.GetUserByIdAsync(userId)
            ?? throw new BadHttpRequestException("Пользователь не найден", 404);

        var productId = Guid.Parse(id);
        var product = await productRepository.GetProductByIdAsync(productId) 
            ?? throw new BadHttpRequestException("Продукт не найден", 404);

        var existingInteraction = await interactionRepository.GetInteractionByUserAndProductAsync(userId, productId);
        if (existingInteraction is not null && existingInteraction.InteractionType == InteractionTypeEnum.Like)
        {
            throw new BadHttpRequestException("Already liked");
        }

        var interaction = new ProductInteraction
        {
            UserId = userId,
            User = user,
            ProductId = productId,
            Product = product,
            InteractionType = InteractionTypeEnum.Like,
            InteractedAt = DateTime.UtcNow
        };
        await interactionRepository.CreateInteractionAsync(interaction);
    }

    [HttpPost("buy/{id}")]
    public async Task BuyProduct(string id)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        var user = await userRepository.GetUserByIdAsync(userId)
            ?? throw new BadHttpRequestException("Пользователь не найден", 404);

        var productId = Guid.Parse(id);
        var product = await productRepository.GetProductByIdAsync(productId)
            ?? throw new BadHttpRequestException("Продукт не найден", 404);

        // Добавляем в Interactions (Bought)
        var interaction = new ProductInteraction
        {
            UserId = userId,
            User = user,
            ProductId = productId,
            Product = product,
            InteractionType = InteractionTypeEnum.Bought,
            InteractedAt = DateTime.UtcNow
        };
        await interactionRepository.CreateInteractionAsync(interaction);

        // Костыль: Добавляем в Reviews (как положительный отзыв, напр. rating=5, is_recommended=true)
        // Предполагаем skin_type из prefs, но если нет - default
        var prefs = user.UserPreferences;  // Нужно реализовать helper или инжект репо
        var skinType = prefs?.SkinType ?? SkinTypeEnum.Normal;  // Default

        var review = new Review
        {
            AuthorId = userId.ToString(),  // Или email? По модели - string AuthorId
            ProductId = product.ProductId,  // string ProductId
            Rating = 5,  // Default для buy
            IsRecommended = true,
            SubmissionTime = DateOnly.FromDateTime(DateTime.UtcNow),
            ReviewText = "Auto-generated from buy",  // Заглушка
            SkinType = skinType
        };
        await reviewRepository.CreateReviewAsync(review);

        // TODO: Позже - вызвать ML add_review для обновления модели
    }
}