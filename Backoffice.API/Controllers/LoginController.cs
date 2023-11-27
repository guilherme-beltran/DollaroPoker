using Backoffice.Application.Interfaces.Users;
using Backoffice.Application.UseCases.Login;
using Backoffice.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backoffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Teste docs
        /// </summary>
        /// <param name="request"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult<Response>> Login([FromBody] LoginCommand request,
                                                             [FromServices] ILoginHandler handler)
        {
            var response = await handler.Handle(request);

            if (response.IsFailure && response.Error.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            if (response.IsFailure && response.Error.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            return Ok(response);
        }
    }
}
