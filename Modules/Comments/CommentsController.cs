using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using blog_backend.Modules.Comments.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_backend.Modules.Comments;

[ApiController]
[Route("api/blogs/{blogId:guid}/comments")]
public sealed class CommentsController(
    ICommentsService commentsService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    [ProducesResponseType<CommentResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentResponse>> CreateComment(
        Guid blogId,
        [FromBody] CreateCommentRequest request,
        CancellationToken cancellationToken)
    {
        var subject = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!Guid.TryParse(subject, out var authorId))
        {
            return Unauthorized();
        }

        var comment = await commentsService.CreateCommentAsync(
            blogId,
            authorId,
            request,
            cancellationToken);

        if (comment is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Blog not found",
                Detail = "The requested blog does not exist."
            });
        }

        return Created(
            $"/api/blogs/{blogId}/comments/{comment.Id}",
            comment);
    }
}
