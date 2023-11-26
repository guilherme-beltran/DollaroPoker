using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Infra.Contexts.Backoffice;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.Infra.Repositories;

internal sealed class PunterRepository : IPunterRepository
{
    private readonly BackofficeContext _context;

    public PunterRepository(BackofficeContext context) => _context = context;

    public async Task<Punter?> GetByIdAsync(int id)
        => await _context
                    .Punters
                    .Where(x => x.PunterId == id)
                    .FirstOrDefaultAsync();

    public async Task<bool> IsRegisteredAsync(string username)
        => await _context
             .Punters
             .AnyAsync(x => x.Username == username);

    public async Task Insert(Punter punter, CancellationToken cancellationToken) => await _context.Punters.AddAsync(punter, cancellationToken);
}
