namespace blog_backend.Modules.Blogs.DTOs;

public sealed record BlogListResponse(
    IReadOnlyList<BlogSummaryResponse> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);
