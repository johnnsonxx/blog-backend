using System.ComponentModel.DataAnnotations;

namespace blog_backend.Modules.Blogs.DTOs;

public sealed class CreateBlogRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Title cannot be blank.")]
    public string Title { get; init; } = string.Empty;

    [Required]
    [StringLength(200, MinimumLength = 1)]
    [RegularExpression(
        @"^[a-z0-9]+(?:-[a-z0-9]+)*$",
        ErrorMessage = "Slug may contain lowercase letters, numbers, and single hyphens only.")]
    public string Slug { get; init; } = string.Empty;

    [Required]
    [RegularExpression(@"[\s\S]*\S[\s\S]*", ErrorMessage = "Content cannot be blank.")]
    public string Content { get; init; } = string.Empty;
}
