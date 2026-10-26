using blog_backend.Modules.Blogs.DTOs;

namespace blog_backend.Modules.Blogs;

public interface IBlogsService
{
    object GetModuleStatus();

    Task<BlogListResponse> GetBlogsAsync(
        GetBlogsQuery query,
        CancellationToken cancellationToken = default);

    Task<BlogResponse?> GetBlogBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);

    Task<BlogResponse?> CreateBlogAsync(
        Guid authorId,
        CreateBlogRequest request,
        CancellationToken cancellationToken = default);

    Task<DeleteBlogResult> DeleteBlogAsync(
        Guid blogId,
        Guid requesterId,
        CancellationToken cancellationToken = default);
}
