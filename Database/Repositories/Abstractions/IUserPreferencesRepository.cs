using CosmeticsRecommendationSystem.Database.Models;

namespace CosmeticsRecommendationSystem.Database.Repositories.Abstractions;

public interface IUserPreferencesRepository
{
    Task<UserPreferences?> GetPreferencesByUserIdAsync(Guid userId);
    Task CreatePreferencesAsync(UserPreferences prefs);
    Task UpdatePreferencesAsync(UserPreferences prefs);
}