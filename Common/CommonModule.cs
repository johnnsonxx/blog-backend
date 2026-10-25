using blog_backend.Common.Guards;
using Microsoft.AspNetCore.Authorization;

namespace blog_backend.Common;

public static class CommonModule
{
    public static IServiceCollection AddCommonModule(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionGuard>();

        return services;
    }
}
