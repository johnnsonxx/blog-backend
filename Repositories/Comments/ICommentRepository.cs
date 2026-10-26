using blog_backend.Entities;

namespace blog_backend.Repositories.Comments;

public interface ICommentRepository
{
    Task<bool> BlogExistsAsync(
        Guid blogId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Comment>> GetCommentsByBlogIdAsync(
        Guid blogId,
        CancellationToken cancellationToken = default);

    Task<Comment?> CreateCommentAsync(
        Comment comment,
        CancellationToken cancellationToken = default);
}
