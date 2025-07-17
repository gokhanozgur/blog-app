using MediatR;
using UserService.Application.DTOs.Users;

namespace UserService.Application.Features.Users.Commands;

public class LoginUserCommand : IRequest<string>
{
    public LoginUserDto LoginUserDto { get; set; }

    public LoginUserCommand(LoginUserDto dto)  => LoginUserDto = dto;
}