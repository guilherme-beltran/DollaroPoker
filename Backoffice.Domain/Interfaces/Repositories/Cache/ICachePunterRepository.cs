using Backoffice.Domain.Entities;

namespace Backoffice.Domain.Interfaces.Repositories.Cache;
public interface ICachePunterRepository
{
    Task<Punter> GetByIdAsync(int id);
    Task<Punter> GetByUsernameAsync(string username);
    Task<bool> IsRegisteredAsync(string username);
    Task Insert(Punter punter, CancellationToken cancellationToken);
    Task<bool> LockAsync(Punter punter, CancellationToken cancellationToken);
    Task<bool> UnlockAsync(Punter punter, CancellationToken cancellationToken);
}