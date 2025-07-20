using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Users.Queries;

public class GetAllUsersQuery : IRequest<List<UserDto>>
{
    
}