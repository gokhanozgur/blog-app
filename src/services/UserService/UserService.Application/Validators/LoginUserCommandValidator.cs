using FluentValidation;
using UserService.Application.Features.Users.Commands;

namespace UserService.Application.Validators;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.LoginUserDto.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.LoginUserDto.Username).MinimumLength(3);
        RuleFor(x => x.LoginUserDto.Password).NotEmpty().MinimumLength(6);
    }
}