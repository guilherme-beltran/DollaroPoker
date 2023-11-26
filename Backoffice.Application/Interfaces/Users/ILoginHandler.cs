using Backoffice.Application.UseCases.Login;
using Backoffice.Domain.Shared;

namespace Backoffice.Application.Interfaces.Users;

public interface ILoginHandler
{
    Task<Response> Handle(LoginCommand request);
}
