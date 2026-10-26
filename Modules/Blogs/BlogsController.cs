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
}
