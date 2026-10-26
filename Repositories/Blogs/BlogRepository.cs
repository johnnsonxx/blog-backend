using blog_backend.Database;
using blog_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace blog_backend.Repositories.Blogs;

public sealed class BlogRepository(BlogDbContext dbContext) : IBlogRepository
{
    public async Task<IReadOnlyList<Blog>> GetBlogsAsync(
        int skip,
        int take,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Blogs
            .AsNoTracking()
            .OrderByDescending(blog => blog.CreatedAt)
            .ThenByDescending(blog => blog.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountBlogsAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.Blogs.CountAsync(cancellationToken);
    }

    public Task<Blog?> FindBlogBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Blogs
            .AsNoTracking()
            .SingleOrDefaultAsync(blog => blog.Slug == slug, cancellationToken);
    }

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

    public Task<Blog?> FindBlogByIdAsync(
        Guid blogId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Blogs
            .AsNoTracking()
            .SingleOrDefaultAsync(blog => blog.Id == blogId, cancellationToken);
    }

    public async Task<bool> DeleteBlogAsync(
        Guid blogId,
        CancellationToken cancellationToken = default)
    {
        var deletedRows = await dbContext.Blogs
            .Where(blog => blog.Id == blogId)
            .ExecuteDeleteAsync(cancellationToken);

        return deletedRows > 0;
    }
}
