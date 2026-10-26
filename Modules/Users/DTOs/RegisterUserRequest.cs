using System.ComponentModel.DataAnnotations;

namespace blog_backend.Modules.Users.DTOs;

public sealed class RegisterUserRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Display name cannot be blank.")]
    public string DisplayName { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(320)]
    public string Email { get; init; } = string.Empty;

    [Required]
    [StringLength(128, MinimumLength = 8)]
    public string Password { get; init; } = string.Empty;
}
