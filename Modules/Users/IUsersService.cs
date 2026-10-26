using blog_backend.Modules.Users.DTOs;

namespace blog_backend.Modules.Users;

public interface IUsersService
{
    object GetModuleStatus();

    Task<UserResponse?> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);

    Task<LoginResponse?> LoginAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken = default);
}
