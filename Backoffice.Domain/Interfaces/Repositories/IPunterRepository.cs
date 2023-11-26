using Backoffice.Domain.Entities;

namespace Backoffice.Domain.Interfaces.Repositories;

public interface IPunterRepository
{
    Task<bool> IsRegisteredAsync(string username);
    Task Insert(Punter punter, CancellationToken cancellationToken);
}
