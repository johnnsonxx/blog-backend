using blog_backend.Shared.Passwords;
using blog_backend.Shared.Tokens;

namespace blog_backend.Shared;

public static class SharedModule
{
    public static IServiceCollection AddSharedModule(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHashService, AspNetCorePasswordHashService>();
        services.AddSingleton<ITokenBuilder, SecureTokenBuilder>();
        return services;
    }
}
