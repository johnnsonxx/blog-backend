using blog_backend.Entities;
using blog_backend.Repositories.Abstractions;

namespace blog_backend.Repositories.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
}
