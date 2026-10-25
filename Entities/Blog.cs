namespace blog_backend.Entities;

public sealed class Blog
{
    public Guid Id { get; init; }

    public Guid AuthorId { get; init; }

    public required string Title { get; set; }

    public required string Slug { get; set; }

    public required string Content { get; set; }

    public string Status { get; set; } = "draft";

    public DateTimeOffset? PublishedAt { get; set; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; set; }

    public User Author { get; set; } = null!;

    public ICollection<Comment> Comments { get; } = [];
}
