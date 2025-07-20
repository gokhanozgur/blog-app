using MediatR;

namespace BlogService.Application.Features.Comments.Commands;

public class DeleteCommentCommand : IRequest<bool>
{
    public string Id { get; set; }
}