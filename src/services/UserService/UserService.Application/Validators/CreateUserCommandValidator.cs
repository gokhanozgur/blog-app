using FluentValidation;
using UserService.Application.Features.Users.Commands;

namespace UserService.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.CreateUserDto.Firstname).NotEmpty().MinimumLength(3);
        RuleFor(x => x.CreateUserDto.Lastname).NotEmpty().MinimumLength(3);
        RuleFor(x => x.CreateUserDto.UserName).NotEmpty().MinimumLength(3);
        RuleFor(x => x.CreateUserDto.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.CreateUserDto.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.CreateUserDto.Role).NotEmpty();
    }
}