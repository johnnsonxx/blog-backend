using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace blog_backend.Database;

public static class DatabaseModule
{
    public static IServiceCollection AddDatabaseModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(
            configuration.GetSection(DatabaseOptions.SectionName));

        services.AddDbContext<BlogDbContext>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider
                .GetRequiredService<IOptions<DatabaseOptions>>()
                .Value;

            if (string.IsNullOrWhiteSpace(databaseOptions.ConnectionString))
            {
                throw new InvalidOperationException(
                    "Database:ConnectionString must be configured.");
            }

            options.UseNpgsql(databaseOptions.ConnectionString);
        });

        return services;
    }
}
