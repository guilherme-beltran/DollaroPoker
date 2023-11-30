using Backoffice.Domain.Entities;

namespace Backoffice.Domain.Interfaces.Repositories;

public interface IPunterRepository
{
    Task<Punter> GetByIdAsync(int id);
    Task<Punter> GetByUsernameAsync(string username);
    Task<bool> IsRegisteredAsync(string username);
    Task Insert(Punter punter, CancellationToken cancellationToken);
    Task<bool> Lock(Punter punter, CancellationToken cancellationToken);
    Task<bool> Unlock(Punter punter, CancellationToken cancellationToken);
}
