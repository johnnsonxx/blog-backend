using blog_backend.Database;
using blog_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace blog_backend.Repositories.Comments;

public sealed class CommentRepository(
    BlogDbContext dbContext) : ICommentRepository
{
    public async Task<Comment?> CreateCommentAsync(
        Comment comment,
        CancellationToken cancellationToken = default)
    {
        var blogExists = await dbContext.Blogs.AnyAsync(
            blog => blog.Id == comment.BlogId,
            cancellationToken);

        if (!blogExists)
        {
            return null;
        }

        dbContext.Comments.Add(comment);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return comment;
        }
        catch (DbUpdateException exception)
            when (exception.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.ForeignKeyViolation,
                ConstraintName: "fk_comments_blog"
            })
        {
            dbContext.Entry(comment).State = EntityState.Detached;
            return null;
        }
    }
}
