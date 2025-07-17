using MediatR;
using UserService.Application.Exceptions;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Interfaces;

namespace UserService.Application.Features.Users.Handlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.LoginUserDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginUserDto.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid credentials");

        return _jwtTokenGenerator.GenerateToken(user.Id, user.UserName, user.Email, user.Role.ToString());
    }
}