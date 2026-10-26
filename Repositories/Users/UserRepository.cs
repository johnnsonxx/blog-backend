using blog_backend.Database;
using blog_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace blog_backend.Repositories.Users;

public sealed class UserRepository(BlogDbContext dbContext) : IUserRepository
{
    public Task<User?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(
                user => user.Email == normalizedEmail,
                cancellationToken);
    }

    public async Task<User?> CreateUserAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        dbContext.Users.Add(user);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }
        catch (DbUpdateException exception)
            when (exception.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.UniqueViolation,
                ConstraintName: "ux_users_email"
            })
        {
            dbContext.Entry(user).State = EntityState.Detached;
            return null;
        }
    }
}
