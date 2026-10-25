using Microsoft.AspNetCore.Mvc;

namespace blog_backend.Modules.Blogs;

[ApiController]
[Route("api/blogs")]
public sealed class BlogsController(IBlogsService blogsService) : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health() => Ok(blogsService.GetModuleStatus());
}
