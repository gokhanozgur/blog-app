using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<UserDto>
{
    public CreateUserDto CreateUserDto { get; set; }
    public CreateUserCommand(CreateUserDto dto) => CreateUserDto = dto;
}