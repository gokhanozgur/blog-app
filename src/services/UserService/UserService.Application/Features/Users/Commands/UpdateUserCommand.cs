using MediatR;
using UserService.Application.DTOs;
using UserService.Application.DTOs.Users;

namespace UserService.Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest<UserDto>
{
    public UpdateUserDto UpdateUserDto { get; set; }
    public UpdateUserCommand(UpdateUserDto dto) => UpdateUserDto = dto;
}