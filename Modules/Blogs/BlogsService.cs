using blog_backend.Entities;
using blog_backend.Modules.Blogs.DTOs;
using blog_backend.Repositories.Blogs;

namespace blog_backend.Modules.Blogs;

public sealed class BlogsService(IBlogRepository blogRepository) : IBlogsService
{
    public object GetModuleStatus() => new { module = "blogs", status = "ok" };

    public async Task<BlogListResponse> GetBlogsAsync(
        GetBlogsQuery query,
        CancellationToken cancellationToken = default)
    {
        var skip = (query.Page - 1) * query.PageSize;
        var blogs = await blogRepository.GetBlogsAsync(
            skip,
            query.PageSize,
            cancellationToken);
        var totalCount = await blogRepository.CountBlogsAsync(cancellationToken);

        var items = blogs
            .Select(blog => new BlogSummaryResponse(
                blog.Id,
                blog.AuthorId,
                blog.Title,
                blog.Slug,
                blog.Status,
                blog.PublishedAt,
                blog.CreatedAt,
                blog.UpdatedAt))
            .ToList();

        var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

        return new BlogListResponse(
            items,
            query.Page,
            query.PageSize,
            totalCount,
            totalPages);
    }

    public async Task<BlogResponse?> GetBlogBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default)
    {
        var blog = await blogRepository.FindBlogBySlugAsync(
            slug.Trim().ToLowerInvariant(),
            cancellationToken);

        return blog is null ? null : ToResponse(blog);
    }

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

        return createdBlog is null ? null : ToResponse(createdBlog);
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

    private static BlogResponse ToResponse(Blog blog)
    {
        return new BlogResponse(
            blog.Id,
            blog.AuthorId,
            blog.Title,
            blog.Slug,
            blog.Content,
            blog.Status,
            blog.PublishedAt,
            blog.CreatedAt,
            blog.UpdatedAt);
    }
}
