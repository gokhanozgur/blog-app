using MediatR;
using UserService.Application.DTOs;
using UserService.Application.DTOs.Users;

namespace UserService.Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest<UserDto>
{
    public Guid UserId { get; set; }
    public UpdateUserDto UpdateUserDto { get; set; }

    public UpdateUserCommand(Guid userId, UpdateUserDto dto)
    {
        UserId = userId;
        UpdateUserDto = dto;
    }
}