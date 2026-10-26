using blog_backend.Common.Authentication;
using blog_backend.Common.Guards;
using Microsoft.AspNetCore.Authorization;

namespace blog_backend.Common;

public static class CommonModule
{
    public static IServiceCollection AddCommonModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddJwtAuthentication(configuration);
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionGuard>();

        return services;
    }
}
