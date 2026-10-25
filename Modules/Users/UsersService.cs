namespace blog_backend.Modules.Users;

public sealed class UsersService : IUsersService
{
    public object GetModuleStatus() => new { module = "users", status = "ok" };
}
