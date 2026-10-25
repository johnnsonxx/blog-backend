namespace blog_backend.Modules.Users.DTOs;

public sealed record UserResponse(Guid Id, string DisplayName, string Email);
