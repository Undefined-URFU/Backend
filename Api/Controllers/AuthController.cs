using CosmeticsRecommendationSystem.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsRecommendationSystem.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task Register([FromBody] AuthRequestDto request)
    {
        await Task.CompletedTask;
    }

    [HttpPost]
    [Route("login")]
    public async Task<TokenDto> Login([FromBody] AuthRequestDto request)
    {
        await Task.CompletedTask;
        return new TokenDto(Token: "tokenstub");
    }
}
