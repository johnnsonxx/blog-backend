using System.ComponentModel.DataAnnotations;

namespace blog_backend.Modules.Blogs.DTOs;

public sealed class GetBlogsQuery
{
    [Range(1, 1_000_000)]
    public int Page { get; init; } = 1;

    [Range(1, 100)]
    public int PageSize { get; init; } = 10;
}
