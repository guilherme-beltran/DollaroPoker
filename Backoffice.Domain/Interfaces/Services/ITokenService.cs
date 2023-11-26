using Backoffice.Domain.Shared;

namespace Backoffice.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(LoginResponse response);
}
