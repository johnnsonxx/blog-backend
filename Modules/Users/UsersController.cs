using Microsoft.AspNetCore.Mvc;

namespace blog_backend.Modules.Users;

[ApiController]
[Route("api/users")]
public sealed class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health() => Ok(usersService.GetModuleStatus());
}
