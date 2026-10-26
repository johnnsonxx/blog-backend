using blog_backend.Entities;

namespace blog_backend.Repositories.Blogs;

public interface IBlogRepository
{
    Task<Blog?> CreateBlogAsync(
        Blog blog,
        CancellationToken cancellationToken = default);
}
