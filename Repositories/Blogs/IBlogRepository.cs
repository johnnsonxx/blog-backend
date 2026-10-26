using blog_backend.Entities;

namespace blog_backend.Repositories.Blogs;

public interface IBlogRepository
{
    Task<IReadOnlyList<Blog>> GetBlogsAsync(
        int skip,
        int take,
        CancellationToken cancellationToken = default);

    Task<int> CountBlogsAsync(CancellationToken cancellationToken = default);

    Task<Blog?> FindBlogBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);

    Task<Blog?> CreateBlogAsync(
        Blog blog,
        CancellationToken cancellationToken = default);

    Task<Blog?> FindBlogByIdAsync(
        Guid blogId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteBlogAsync(
        Guid blogId,
        CancellationToken cancellationToken = default);
}
