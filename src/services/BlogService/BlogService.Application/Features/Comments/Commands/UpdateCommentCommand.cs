using BlogService.Application.DTOs.Comments;
using MediatR;

namespace BlogService.Application.Features.Comments.Commands;

public class UpdateCommentCommand : IRequest<CommentDto>
{
    public string CommentId { get; set; }
    public UpdateCommentDto UpdateCommentDto { get; set; }

    public UpdateCommentCommand(string id, UpdateCommentDto dto)
    {
        CommentId = id;
        UpdateCommentDto = dto;
    }
}