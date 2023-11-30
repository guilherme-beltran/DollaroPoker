using Backoffice.Application.UseCases.Punters.Lock;
using Backoffice.Domain.Shared;

namespace Backoffice.Application.Interfaces.Punters;

public interface ILockPunterHandler
{
    Task<Response> Handle(LockPunterCommand command, CancellationToken cancellationToken);
}
