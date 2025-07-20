using BlogService.Application.DTOs.Comments;
using MediatR;

namespace BlogService.Application.Features.Comments.Commands;

public class CreateCommentCommand : IRequest<CommentDto>
{
    public CreateCommentDto CreateCommentDto { get; set; }
    public CreateCommentCommand(CreateCommentDto dto) =>  CreateCommentDto = dto;
}