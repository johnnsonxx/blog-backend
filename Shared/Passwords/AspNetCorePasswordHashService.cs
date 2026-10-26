using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace blog_backend.Shared.Passwords;

public sealed class AspNetCorePasswordHashService(
    IOptions<PasswordHasherOptions> options) : IPasswordHashService
{
    private static readonly object UserContext = new();

    private readonly PasswordHasher<object> _passwordHasher = new(options);

    public string Hash(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        return _passwordHasher.HashPassword(UserContext, password);
    }

    public bool Verify(string passwordHash, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        return _passwordHasher.VerifyHashedPassword(
            UserContext,
            passwordHash,
            password) is not PasswordVerificationResult.Failed;
    }
}
