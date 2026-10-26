namespace blog_backend.Modules.Blogs.DTOs;

public sealed record BlogResponse(
    Guid Id,
    Guid AuthorId,
    string Title,
    string Slug,
    string Content,
    string Status,
    DateTimeOffset? PublishedAt,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
