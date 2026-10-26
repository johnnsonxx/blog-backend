using blog_backend.Entities;

namespace blog_backend.Repositories.Comments;

public interface ICommentRepository
{
    Task<Comment?> CreateCommentAsync(
        Comment comment,
        CancellationToken cancellationToken = default);
}
