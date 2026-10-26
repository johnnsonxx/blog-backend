namespace blog_backend.Shared.Tokens;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; init; } = "blog-backend";

    public string Audience { get; init; } = "blog-frontend";

    public string SigningKey { get; init; } = string.Empty;

    public int ExpirationMinutes { get; init; } = 60;
}
