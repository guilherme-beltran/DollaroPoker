using Backoffice.Domain.DTO;
using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Interfaces.Repositories.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Backoffice.Infra.Repositories.Cache;

internal sealed class CacheUserRepository : ICacheUserRepository
{
    private readonly IUserRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CacheUserRepository(IUserRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        string key = $"user-{id}";

        return await _memoryCache.GetOrCreateAsync(
            key: key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
                return _decorated.GetByIdAsync(id);
            });
    }

    public Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    } 

    public Task<User> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task Insert(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsRegisteredAsync(string username)
    {
        throw new NotImplementedException();
    }
}
