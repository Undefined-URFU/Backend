using CosmeticsRecommendationSystem.Database.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmeticsRecommendationSystem.Database.Models;

public class Review
{
    public Guid Id { get; set; }
    public required string AuthorId { get; set; }
    public required string ProductId { get; set; }
    public required int Rating { get; set; }
    public required bool IsRecommended { get; set; }
    public required DateOnly SubmissionTime { get; set; }
    public required string ReviewText { get; set; }

    [Column(TypeName = "varchar(15)")]
    public required SkinTypeEnum SkinType { get; set; }
}
