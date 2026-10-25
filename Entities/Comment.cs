namespace blog_backend.Entities;

public sealed class Comment
{
    public Guid Id { get; init; }

    public Guid BlogId { get; init; }

    public Guid AuthorId { get; init; }

    public required string Content { get; set; }

    public string Status { get; set; } = "visible";

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; set; }

    public Blog Blog { get; set; } = null!;

    public User Author { get; set; } = null!;
}
