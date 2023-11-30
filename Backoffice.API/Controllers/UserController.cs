using Backoffice.Application.Interfaces.Users;
using Backoffice.Application.UseCases.Users.Create;
using Backoffice.Domain.DTO;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backoffice.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetAll([FromServices] IUserRepository _repository,
                                                        CancellationToken cancellationToken)
        {
            var users = await _repository.GetAll(cancellationToken);

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Insert([FromBody] CreateUserCommand request,
                                                         [FromServices] ICreateUserHandler handler,
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
}
