using CosmeticsRecommendationSystem.Database.Models;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsRecommendationSystem.Database.Repositories;

public class UserPreferencesRepository(DatabaseContext database) : IUserPreferencesRepository
{
    public async Task<UserPreferences?> GetPreferencesByUserIdAsync(Guid userId)
    {
        return await database.UsersPreferences.FirstOrDefaultAsync(up => up.UserId == userId);
    }

    public async Task CreatePreferencesAsync(UserPreferences prefs)
    {
        database.UsersPreferences.Add(prefs);
        await database.SaveChangesAsync();
    }

    public async Task UpdatePreferencesAsync(UserPreferences prefs)
    {
        database.UsersPreferences.Update(prefs);
        await database.SaveChangesAsync();
    }
}