namespace blog_backend.Modules.Blogs;

public static class BlogsModule
{
    public static IServiceCollection AddBlogsModule(this IServiceCollection services)
    {
        services.AddScoped<IBlogsService, BlogsService>();
        return services;
    }
}
