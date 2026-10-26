namespace blog_backend.Modules.Blogs.DTOs;

public sealed record BlogSummaryResponse(
    Guid Id,
    Guid AuthorId,
    string Title,
    string Slug,
    string Status,
    DateTimeOffset? PublishedAt,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
