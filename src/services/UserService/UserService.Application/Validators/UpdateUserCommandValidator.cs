using FluentValidation;
using UserService.Application.Features.Users.Commands;

namespace UserService.Application.Validators;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UpdateUserDto.Firstname).NotEmpty().MinimumLength(3);
        RuleFor(x => x.UpdateUserDto.Lastname).NotEmpty().MinimumLength(3);
        RuleFor(x => x.UpdateUserDto.UserName).NotEmpty().MinimumLength(3);
        RuleFor(x => x.UpdateUserDto.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.UpdateUserDto.Password).NotEmpty().MinimumLength(6);
    }
}