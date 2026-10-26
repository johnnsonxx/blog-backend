using blog_backend.Modules.Comments.DTOs;

namespace blog_backend.Modules.Comments;

public interface ICommentsService
{
    Task<IReadOnlyList<CommentListItemResponse>?> GetCommentsByBlogIdAsync(
        Guid blogId,
        CancellationToken cancellationToken = default);

    Task<CommentResponse?> CreateCommentAsync(
        Guid blogId,
        Guid authorId,
        CreateCommentRequest request,
        CancellationToken cancellationToken = default);
}
