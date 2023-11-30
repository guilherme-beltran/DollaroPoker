using Backoffice.Domain.DTO;
using Backoffice.Domain.Entities;

namespace Backoffice.Domain.Interfaces.Repositories.Cache;

public interface ICacheUserRepository
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> GetByUsernameAsync(string username);
    Task<bool> IsRegisteredAsync(string username);
    Task Insert(User user, CancellationToken cancellationToken);
}
