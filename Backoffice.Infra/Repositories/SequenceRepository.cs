using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Infra.Contexts.Backoffice;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.Infra.Repositories;

internal sealed class SequenceRepository : ISequenceRepository
{
    private readonly BackofficeContext _context;

    public SequenceRepository(BackofficeContext context) => _context = context;

    public async Task<Sequence?> GetSequencePunterAsync()
    => await _context
                .Sequences
                .Where(x => x.Name == "PUN_ID_SEQ")
                .FirstOrDefaultAsync();

    public async Task<Sequence?> GetSequenceUserAsync()
        => await _context
                .Sequences
                .Where(x => x.Name == "ALG_ID_SEQ")
                .FirstOrDefaultAsync();

    public async Task<bool> Update(Sequence sequence)
    {
        var updated = await _context
            .Sequences
            .Where(x => x.Name == sequence.Name)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(u => u.Name, sequence.Name)
                .SetProperty(u => u.NextSequence, sequence.NextSequence));

        return updated != 0;
    }
}
