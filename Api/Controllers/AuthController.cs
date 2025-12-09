using CosmeticsRecommendationSystem.Api.Abstractions;
using CosmeticsRecommendationSystem.Api.Dtos;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsRecommendationSystem.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IJwtService jwtService, IUserRepository userRepository): ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task Register([FromBody] AuthRequestDto request)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email);
        if (user is not null)
        {
            throw new BadHttpRequestException("Пользователь с таким email уже существует");
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<TokenDto> Login([FromBody] AuthRequestDto request)
    {
        await Task.CompletedTask;
        return new TokenDto(Token: jwtService.GenerateToken("user_id"));
    }
}
