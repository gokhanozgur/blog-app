using MediatR;

namespace UserService.Application.Features.Users.Commands;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public DeleteUserCommand(Guid userId) => UserId = userId;
}