namespace blog_backend.Repositories;

public static class RepositoriesModule
{
    public static IServiceCollection AddRepositoriesModule(this IServiceCollection services)
    {
        // Bind repository interfaces after a database provider has been selected.
        return services;
    }
}
