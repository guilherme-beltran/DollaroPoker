using Backoffice.Domain.Entities;

namespace Backoffice.Domain.Interfaces.Repositories;

public interface ISequenceRepository
{
    Task<Sequence?> GetSequenceUserAsync();
    Task<Sequence?> GetSequencePunterAsync();
    Task<bool> Update(Sequence sequence);
}
