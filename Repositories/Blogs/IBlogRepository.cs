using blog_backend.Entities;
using blog_backend.Repositories.Abstractions;

namespace blog_backend.Repositories.Blogs;

public interface IBlogRepository : IRepository<Blog, Guid>
{
    Task<Blog?> FindBySlugAsync(string slug, CancellationToken cancellationToken = default);
}
