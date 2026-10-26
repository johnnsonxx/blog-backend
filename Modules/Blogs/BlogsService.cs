using blog_backend.Entities;
using blog_backend.Modules.Blogs.DTOs;
using blog_backend.Repositories.Blogs;

namespace blog_backend.Modules.Blogs;

public sealed class BlogsService(IBlogRepository blogRepository) : IBlogsService
{
    public object GetModuleStatus() => new { module = "blogs", status = "ok" };

    public async Task<BlogResponse?> CreateBlogAsync(
        Guid authorId,
        CreateBlogRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var blog = new Blog
        {
            Id = Guid.NewGuid(),
            AuthorId = authorId,
            Title = request.Title.Trim(),
            Slug = request.Slug.Trim().ToLowerInvariant(),
            Content = request.Content.Trim(),
            Status = "draft",
            PublishedAt = null,
            CreatedAt = now,
            UpdatedAt = now
        };

        var createdBlog = await blogRepository.CreateBlogAsync(
            blog,
            cancellationToken);

        return createdBlog is null
            ? null
            : new BlogResponse(
                createdBlog.Id,
                createdBlog.AuthorId,
                createdBlog.Title,
                createdBlog.Slug,
                createdBlog.Content,
                createdBlog.Status,
                createdBlog.PublishedAt,
                createdBlog.CreatedAt,
                createdBlog.UpdatedAt);
    }

    public async Task<DeleteBlogResult> DeleteBlogAsync(
        Guid blogId,
        Guid requesterId,
        CancellationToken cancellationToken = default)
    {
        var blog = await blogRepository.FindBlogByIdAsync(blogId, cancellationToken);

        if (blog is null)
        {
            return DeleteBlogResult.NotFound;
        }

        if (blog.AuthorId != requesterId)
        {
            return DeleteBlogResult.Forbidden;
        }

        var deleted = await blogRepository.DeleteBlogAsync(blogId, cancellationToken);
        return deleted ? DeleteBlogResult.Deleted : DeleteBlogResult.NotFound;
    }
}
