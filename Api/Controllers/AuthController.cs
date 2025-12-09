using Microsoft.AspNetCore.Mvc;

namespace CosmeticsRecommendationSystem.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public int Test()
    {
        return 0;
    }
}
