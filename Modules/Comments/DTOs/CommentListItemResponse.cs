namespace blog_backend.Modules.Comments.DTOs;

public sealed record CommentListItemResponse(
    Guid Id,
    Guid BlogId,
    Guid AuthorId,
    string AuthorDisplayName,
    string Content,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
