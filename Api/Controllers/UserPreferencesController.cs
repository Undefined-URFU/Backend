using CosmeticsRecommendationSystem.Api.Dtos;
using CosmeticsRecommendationSystem.Database.Enums;
using CosmeticsRecommendationSystem.Database.Models;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;
using CosmeticsRecommendationSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsRecommendationSystem.Api.Controllers;

[ApiController]
[Route("api/v1/user_preferences")]
[Authorize]
public class UserPreferencesController(IUserPreferencesRepository preferencesRepository, IUserRepository userRepository) : ControllerBase
{
    [HttpGet]
    public async Task<UserPreferencesDto> GetUserPreferences()
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);  // Из JWT claims

        var user = await userRepository.GetUserByIdAsync(userId) 
            ?? throw new BadHttpRequestException("Пользователь не найден", 404);

        var prefs = await preferencesRepository.GetPreferencesByUserIdAsync(userId) 
            ?? throw new BadHttpRequestException("Настройки пользователя не найдены", 404);

        return new UserPreferencesDto(prefs.SkinType.ToString(), prefs.Blacklist);
    }

    [HttpPost]
    public async Task CreateUserPreferences([FromBody] UserPreferencesDto request)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        var user = await userRepository.GetUserByIdAsync(userId) ?? throw new BadHttpRequestException("Пользователь не найден", 404);

        SkinTypeEnum skinType;
        try
        {
            skinType = EnumHelper.Parse<SkinTypeEnum>(request.SkinType);
        }
        catch
        {
            throw new BadHttpRequestException("Отправлен неправильный тип кожи");
        }

        var existingPrefs = await preferencesRepository.GetPreferencesByUserIdAsync(userId);
        if (existingPrefs is not null)
        {
            // Update if exists
            existingPrefs.SkinType = skinType;
            existingPrefs.Blacklist = request.Blacklist;
            await preferencesRepository.UpdatePreferencesAsync(existingPrefs);
        }
        else
        {
            var newPrefs = new UserPreferences
            {
                SkinType = skinType,
                Blacklist = request.Blacklist,
                UserId = userId,
                User = user
            };
            await preferencesRepository.CreatePreferencesAsync(newPrefs);
        }
    }
}