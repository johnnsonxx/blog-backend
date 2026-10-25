namespace blog_backend.Modules.Blogs.DTOs;

public sealed record BlogResponse(Guid Id, string Title, string Slug, DateTimeOffset? PublishedAt);
