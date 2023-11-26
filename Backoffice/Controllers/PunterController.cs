using Backoffice.Application.Interfaces.Punters;
using Backoffice.Application.UseCases.Punters.Create;
using Backoffice.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backoffice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PunterController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response>> Insert([FromBody] CreatePunterCommand request,
                                                    [FromServices] ICreatePunterHandler handler,
                                                    CancellationToken cancellationToken)
    {
        var response = await handler.Handle(request, cancellationToken);

        if (response.Error.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(response);

        if (response.Error.StatusCode == HttpStatusCode.Conflict)
            return Conflict(response);

        if (response.Error.StatusCode == HttpStatusCode.InternalServerError)
            return StatusCode(500, response);

        return Ok(response);
    }
}
