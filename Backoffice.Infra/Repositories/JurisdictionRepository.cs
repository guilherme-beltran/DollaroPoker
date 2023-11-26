using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Infra.Contexts.Backoffice;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.Infra.Repositories;

internal sealed class JurisdictionRepository : IJurisdictionRepository
{
    private readonly BackofficeContext _context;

    public JurisdictionRepository(BackofficeContext context) => _context = context;

    public async Task<Jurisdiction> GetByIdAsync(int id) 
        => await _context
                .Jurisdictions
                .Where(x => x.JurisdictionId == id)
                .FirstOrDefaultAsync();
}
