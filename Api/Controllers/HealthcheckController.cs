using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsRecommendationSystem.Api.Controllers;

[ApiController]
[Route("api/v1/healthcheck")]
public class HealthcheckController
{
    [HttpGet]
    [Route("check")]
    public void Check()
    {

    }

    [HttpGet]
    [Route("check_with_auth")]
    [Authorize]
    public void CheckWithAuth()
    {

    }
}
