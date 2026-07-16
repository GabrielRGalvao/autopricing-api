using Microsoft.AspNetCore.Mvc;

namespace AutoPricing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "AutoPricing API is running."
        });
    }
}