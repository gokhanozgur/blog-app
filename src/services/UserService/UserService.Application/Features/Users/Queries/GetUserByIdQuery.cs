using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid UserId { get; set; }
    public GetUserByIdQuery(Guid userId) => UserId = userId;
}