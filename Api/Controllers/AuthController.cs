using CosmeticsRecommendationSystem.Api.Abstractions;
using CosmeticsRecommendationSystem.Api.Dtos;
using CosmeticsRecommendationSystem.Database.Models;
using CosmeticsRecommendationSystem.Database.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsRecommendationSystem.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IJwtService jwtService, IUserRepository userRepository) : ControllerBase
{
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    [HttpPost]
    [Route("register")]
    public async Task Register([FromBody] AuthRequestDto request)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            throw new BadHttpRequestException("Пользователь с таким email уже существует");
        }

        var newUser = new User() { Email = request.Email, HashedPassword = _passwordHasher.HashPassword(null!, request.Password) };
        await userRepository.CreateUserAsync(newUser);
    }

    [HttpPost]
    [Route("login")]
    public async Task<TokenDto> Login([FromBody] AuthRequestDto request)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email) 
            ?? throw new BadHttpRequestException("Пользователя с таким email не существует", 404);

        if (_passwordHasher.VerifyHashedPassword(user, user.HashedPassword, request.Password) is PasswordVerificationResult.Failed)
        {
            throw new BadHttpRequestException("Неправильный пароль");
        }

        return new TokenDto(Token: jwtService.GenerateToken(user.Id.ToString()));
    }
}
