using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using blog_backend.Modules.Blogs.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_backend.Modules.Blogs;

[ApiController]
[Route("api/blogs")]
public sealed class BlogsController(IBlogsService blogsService) : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health() => Ok(blogsService.GetModuleStatus());

    [HttpGet]
    [ProducesResponseType<BlogListResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BlogListResponse>> GetBlogs(
        [FromQuery] GetBlogsQuery query,
        CancellationToken cancellationToken)
    {
        return Ok(await blogsService.GetBlogsAsync(query, cancellationToken));
    }

    [HttpGet("{slug}")]
    [ProducesResponseType<BlogResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogResponse>> GetBlogBySlug(
        string slug,
        CancellationToken cancellationToken)
    {
        var blog = await blogsService.GetBlogBySlugAsync(slug, cancellationToken);

        if (blog is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Blog not found",
                Detail = $"Blog with slug '{slug}' does not exist."
            });
        }

        return Ok(blog);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType<BlogResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<BlogResponse>> CreateBlog(
        [FromBody] CreateBlogRequest request,
        CancellationToken cancellationToken)
    {
        var subject = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!Guid.TryParse(subject, out var authorId))
        {
            return Unauthorized();
        }

        var blog = await blogsService.CreateBlogAsync(
            authorId,
            request,
            cancellationToken);

        if (blog is null)
        {
            return Conflict(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Slug already exists",
                Detail = "A blog with this slug already exists."
            });
        }

        return Created($"/api/blogs/{blog.Slug}", blog);
    }

    [Authorize]
    [HttpDelete("{blogId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBlog(
        Guid blogId,
        CancellationToken cancellationToken)
    {
        var subject = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!Guid.TryParse(subject, out var requesterId))
        {
            return Unauthorized();
        }

        var result = await blogsService.DeleteBlogAsync(
            blogId,
            requesterId,
            cancellationToken);

        return result switch
        {
            DeleteBlogResult.Deleted => NoContent(),
            DeleteBlogResult.NotFound => NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Blog not found",
                Detail = $"Blog '{blogId}' does not exist."
            }),
            DeleteBlogResult.Forbidden => StatusCode(
                StatusCodes.Status403Forbidden,
                new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Detail = "Only the blog author can delete this blog."
                }),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
