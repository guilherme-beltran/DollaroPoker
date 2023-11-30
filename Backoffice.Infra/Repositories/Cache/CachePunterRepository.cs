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
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
                return _decorated.GetByIdAsync(id);
            });
    }

    public async Task<Punter> GetByUsernameAsync(string username)
    {
        string key = $"punter-{username}";

        return await _memoryCache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
                return _decorated.GetByUsernameAsync(username);
            });
    }

    public async Task Insert(Punter punter, CancellationToken cancellationToken)
    {
        string key = $"punter-{punter.Username}";

        _memoryCache.Set(key, punter);
        await _decorated.Insert(punter, cancellationToken);
    }

    public async Task<bool> IsRegisteredAsync(string username)
    {
        string key = $"punter-{username}";

        if (_memoryCache.TryGetValue(key, out bool isRegistered))
        {
            return isRegistered;
        }

        isRegistered = await _decorated.IsRegisteredAsync(username);

        _memoryCache.Set(key, isRegistered, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
        });

        return isRegistered;
    }

    public async Task<bool> LockAsync(Punter punter, CancellationToken cancellationToken)
    {
        string key = $"punter-{punter.Username}";
        var cachePunter = _memoryCache.Get(key);
        bool locked;
        if (cachePunter is null)
        {
            locked = await _decorated.Lock(punter, cancellationToken);
            _memoryCache.Set(key, punter, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });

            return locked;
        }

        _memoryCache.Set(key, punter, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
        });
        locked = true;
        return locked;
    }

    public async Task<bool> UnlockAsync(Punter punter, CancellationToken cancellationToken)
    {
        string key = $"punter-{punter.Username}";
        var cachePunter = _memoryCache.Get(key);
        bool locked;
        if (cachePunter is null)
        {
            locked = await _decorated.Unlock(punter, cancellationToken);
            _memoryCache.Set(key, punter, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });

            return locked;
        }

        _memoryCache.Set(key, punter, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
        });
        locked = true;
        return locked;
    }

}
