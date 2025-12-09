using CosmeticsRecommendationSystem.Database.Models;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsRecommendationSystem.Database.Repositories;

public class UserRepository(DatabaseContext database) : IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await database.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await database.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
}
