using Backoffice.Application.UseCases.Users.Create;
using Backoffice.Domain.Shared;

namespace Backoffice.Application.Interfaces.Users;

public interface ICreateUserHandler
{
    Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken);
}
