using blog_backend.Entities;

namespace blog_backend.Repositories.Users;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<User?> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}
