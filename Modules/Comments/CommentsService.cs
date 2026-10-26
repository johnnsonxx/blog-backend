using blog_backend.Entities;
using blog_backend.Modules.Comments.DTOs;
using blog_backend.Repositories.Comments;

namespace blog_backend.Modules.Comments;

public sealed class CommentsService(
    ICommentRepository commentRepository) : ICommentsService
{
    public async Task<IReadOnlyList<CommentListItemResponse>?> GetCommentsByBlogIdAsync(
        Guid blogId,
        CancellationToken cancellationToken = default)
    {
        if (!await commentRepository.BlogExistsAsync(blogId, cancellationToken))
        {
            return null;
        }

        var comments = await commentRepository.GetCommentsByBlogIdAsync(
            blogId,
            cancellationToken);

        return comments
            .Select(comment => new CommentListItemResponse(
                comment.Id,
                comment.BlogId,
                comment.AuthorId,
                comment.Author.DisplayName,
                comment.Content,
                comment.CreatedAt,
                comment.UpdatedAt))
            .ToList();
    }

    public async Task<CommentResponse?> CreateCommentAsync(
        Guid blogId,
        Guid authorId,
        CreateCommentRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            BlogId = blogId,
            AuthorId = authorId,
            Content = request.Content.Trim(),
            Status = "visible",
            CreatedAt = now,
            UpdatedAt = now
        };

        var createdComment = await commentRepository.CreateCommentAsync(
            comment,
            cancellationToken);

        return createdComment is null
            ? null
            : new CommentResponse(
                createdComment.Id,
                createdComment.BlogId,
                createdComment.AuthorId,
                createdComment.Content,
                createdComment.Status,
                createdComment.CreatedAt,
                createdComment.UpdatedAt);
    }
}
