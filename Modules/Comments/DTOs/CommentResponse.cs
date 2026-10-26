namespace blog_backend.Modules.Comments.DTOs;

public sealed record CommentResponse(
    Guid Id,
    Guid BlogId,
    Guid AuthorId,
    string Content,
    string Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
