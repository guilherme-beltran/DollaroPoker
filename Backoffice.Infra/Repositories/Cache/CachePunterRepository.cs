using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Interfaces.Repositories.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Backoffice.Infra.Repositories.Cache;

public sealed class CachePunterRepository : ICachePunterRepository
{
    private readonly IPunterRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachePunterRepository(IPunterRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task<Punter> GetByIdAsync(int id)
    {
        string key = $"punter-{id}";



        return await _memoryCache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                return _decorated.GetByIdAsync(id);
            });
    }

    public async Task Insert(Punter punter, CancellationToken cancellationToken)
    {
        await _decorated.Insert(punter, cancellationToken);
    }

    public async Task<bool> IsRegisteredAsync(string username)
    {
        return await _decorated.IsRegisteredAsync(username);
    }
}
