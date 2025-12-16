using CosmeticsRecommendationSystem.Database.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmeticsRecommendationSystem.Database.Models;

public class UserPreferences
{
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(15)")]
    public required SkinTypeEnum SkinType { get; set; }

    public required string[] Blacklist { get; set; }
    
    public required Guid UserId { get; set; }
    public required virtual User User { get; set; }
}
