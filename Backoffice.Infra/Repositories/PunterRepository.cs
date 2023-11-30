using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Infra.Contexts.Backoffice;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.Infra.Repositories;

public sealed class PunterRepository : IPunterRepository
{
    private readonly BackofficeContext _context;

    public PunterRepository(BackofficeContext context) => _context = context;

    public async Task<Punter> GetByIdAsync(int id)
        => await _context
                .Punters
                .Where(p => p.PunterId.Equals(id))
                .FirstOrDefaultAsync();

    public async Task<bool> IsRegisteredAsync(string username)
        => await _context
             .Punters
             .AnyAsync(x => x.Username == username);

    public async Task Insert(Punter punter, CancellationToken cancellationToken) => await _context.Punters.AddAsync(punter, cancellationToken);

    public async Task<Punter> GetByUsernameAsync(string username)
        => await _context
                .Punters
                .Where(p => p.Username.Equals(username))
                .FirstOrDefaultAsync();

    public async Task<bool> Lock(Punter punter, CancellationToken cancellationToken)
    {
        var locked = await _context
                    .Punters
                    .Where(p => p.Username == punter.Username)
                    .ExecuteUpdateAsync(x =>
                        x.SetProperty(x => x.Access, punter.Access)
                        .SetProperty(x => x.LockReason, punter.LockReason));

        return locked != 0;
    }

    public async Task<bool> Unlock(Punter punter, CancellationToken cancellationToken)
    {
        var unlocked = await _context
                    .Punters
                    .Where(p => p.Username == punter.Username)
                    .ExecuteUpdateAsync(x =>
                        x.SetProperty(x => x.Access, punter.Access)
                        .SetProperty(x => x.LockReason, punter.LockReason));

        return unlocked != 0;
    }
}
