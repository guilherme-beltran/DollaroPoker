using Backoffice.Domain.Entities;

namespace Backoffice.Domain.Interfaces.Repositories.Cache;
public interface ICachePunterRepository
{
    Task<Punter> GetByIdAsync(int id);
    Task<bool> IsRegisteredAsync(string username);
    Task Insert(Punter punter, CancellationToken cancellationToken);
}