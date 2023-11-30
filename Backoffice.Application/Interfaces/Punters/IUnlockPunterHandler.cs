using Backoffice.Application.UseCases.Punters.Unlock;
using Backoffice.Domain.Shared;

namespace Backoffice.Application.Interfaces.Punters;

public interface IUnlockPunterHandler
{
    Task<Response> Handle(UnlockPunterCommand command, CancellationToken cancellationToken);
}
