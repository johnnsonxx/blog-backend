using System.ComponentModel.DataAnnotations;

namespace blog_backend.Modules.Comments.DTOs;

public sealed class CreateCommentRequest
{
    [Required]
    [StringLength(5000, MinimumLength = 1)]
    [RegularExpression(@"[\s\S]*\S[\s\S]*", ErrorMessage = "Content cannot be blank.")]
    public string Content { get; init; } = string.Empty;
}
