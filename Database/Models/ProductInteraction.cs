using CosmeticsRecommendationSystem.Database.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmeticsRecommendationSystem.Database.Models;

public class ProductInteraction
{
    public Guid Id { get; set; }

    public required Guid UserId { get; set; }
    public required virtual User User { get; set; }

    public required Guid ProductId { get; set; }
    public required virtual Product Product { get; set; }

    [Column(TypeName = "varchar(15)")]
    public required InteractionTypeEnum InteractionType { get; set; }

    public required DateTime InteractedAt { get; set; }
}
