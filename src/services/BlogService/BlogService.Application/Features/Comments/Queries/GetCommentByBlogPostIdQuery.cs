using BlogService.Application.DTOs.Comments;
using MediatR;

namespace BlogService.Application.Features.Comments.Queries;

public class GetCommentByBlogPostIdQuery : IRequest<List<CommentDto>>
{
    public string BlogPostId { get; set; }
}