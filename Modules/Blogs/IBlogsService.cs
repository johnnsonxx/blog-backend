using blog_backend.Modules.Blogs.DTOs;

namespace blog_backend.Modules.Blogs;

public interface IBlogsService
{
    object GetModuleStatus();

    Task<BlogResponse?> CreateBlogAsync(
        Guid authorId,
        CreateBlogRequest request,
        CancellationToken cancellationToken = default);
}
