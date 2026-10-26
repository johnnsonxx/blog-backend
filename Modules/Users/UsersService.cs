using blog_backend.Entities;
using blog_backend.Modules.Users.DTOs;
using blog_backend.Repositories.Users;
using blog_backend.Shared.Passwords;

namespace blog_backend.Modules.Users;

public sealed class UsersService(
    IUserRepository userRepository,
    IPasswordHashService passwordHashService) : IUsersService
{
    public object GetModuleStatus() => new { module = "users", status = "ok" };

    public async Task<UserResponse?> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email.Trim().ToLowerInvariant(),
            PasswordHash = string.Empty,
            DisplayName = request.DisplayName.Trim(),
            CreatedAt = now,
            UpdatedAt = now
        };

        user.PasswordHash = passwordHashService.Hash(request.Password);

        var createdUser = await userRepository.CreateUserAsync(user, cancellationToken);

        return createdUser is null
            ? null
            : new UserResponse(
                createdUser.Id,
                createdUser.DisplayName,
                createdUser.Email);
    }
}
