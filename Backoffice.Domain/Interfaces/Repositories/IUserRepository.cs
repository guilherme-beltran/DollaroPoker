using Backoffice.Domain.DTO;
using Backoffice.Domain.Entities;

namespace Backoffice.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserDTO>> GetAll(CancellationToken cancellationToken);
    Task<User> GetByIdAsync(int id);
    Task<User> GetByUsernameAsync(string username);
    Task<bool> IsRegisteredAsync(string username);
    Task Insert(User user, CancellationToken cancellationToken);
}
