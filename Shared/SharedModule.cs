using blog_backend.Shared.Passwords;
using blog_backend.Shared.Tokens;

namespace blog_backend.Shared;

public static class SharedModule
{
    public static IServiceCollection AddSharedModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.AddSingleton<IPasswordHashService, AspNetCorePasswordHashService>();
        services.AddSingleton<IAccessTokenService, JwtAccessTokenService>();
        services.AddSingleton<ITokenBuilder, SecureTokenBuilder>();
        return services;
    }
}
