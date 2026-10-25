namespace blog_backend.Entities;

public sealed class User
{
    public Guid Id { get; init; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public required string DisplayName { get; set; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<Blog> Blogs { get; } = [];

    public ICollection<Comment> Comments { get; } = [];
}
