using blog_backend.Modules.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace blog_backend.Modules.Users;

[ApiController]
[Route("api/users")]
public sealed class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health() => Ok(usersService.GetModuleStatus());

    [HttpPost("register")]
    [ProducesResponseType<UserResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponse>> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = await usersService.RegisterAsync(request, cancellationToken);

        if (user is null)
        {
            return Conflict(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "電子郵件已註冊",
                Detail = "此電子郵件地址已存在帳戶。"
            });
        }

        return Created($"/api/users/{user.Id}", user);
    }
}
