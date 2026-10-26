using blog_backend.Repositories.Blogs;
using blog_backend.Repositories.Users;

namespace blog_backend.Repositories;

public static class RepositoriesModule
{
    public static IServiceCollection AddRepositoriesModule(this IServiceCollection services)
    {
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
