using Backoffice.Domain.Entities;

namespace Backoffice.Domain.Interfaces.Repositories;

public interface IJurisdictionRepository
{
    Task<Jurisdiction> GetByIdAsync(int id);
}
