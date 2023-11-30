using Microsoft.AspNetCore.Mvc;

namespace Backoffice.API.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get()
    {
        return Ok($"Funcionando!");
    }
}
