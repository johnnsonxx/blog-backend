namespace blog_backend.Modules.Users.DTOs;

public sealed record LoginResponse(
    string AccessToken,
    DateTimeOffset ExpiresAt,
    UserResponse User);
