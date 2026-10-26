namespace blog_backend.Shared.Tokens;

public sealed record AccessTokenResult(string Token, DateTimeOffset ExpiresAt);
