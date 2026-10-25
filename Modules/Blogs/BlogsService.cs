namespace blog_backend.Modules.Blogs;

public sealed class BlogsService : IBlogsService
{
    public object GetModuleStatus() => new { module = "blogs", status = "ok" };
}
