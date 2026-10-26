using blog_backend.Database;
using blog_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace blog_backend.Repositories.Blogs;

public sealed class BlogRepository(BlogDbContext dbContext) : IBlogRepository
{
    public async Task<Blog?> CreateBlogAsync(
        Blog blog,
        CancellationToken cancellationToken = default)
    {
        dbContext.Blogs.Add(blog);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return blog;
        }
        catch (DbUpdateException exception)
            when (exception.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.UniqueViolation,
                ConstraintName: "ux_blogs_slug"
            })
        {
            dbContext.Entry(blog).State = EntityState.Detached;
            return null;
        }
    }
}
