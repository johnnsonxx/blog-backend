namespace blog_backend.Modules.Comments;

public static class CommentsModule
{
    public static IServiceCollection AddCommentsModule(this IServiceCollection services)
    {
        services.AddScoped<ICommentsService, CommentsService>();
        return services;
    }
}
