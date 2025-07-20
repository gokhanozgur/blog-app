using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Application.DTOs;
using UserService.Application.Exceptions;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Interfaces;

namespace UserService.Application.Features.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.CreateUserDto.Email);
        // TODO should check username also.
        if (user != null)
        {
            _logger.LogWarning("User already exists.");
            throw new UserAlreadyExistException();
        }
        
        var newUser = _mapper.Map<UserService.Domain.Entities.Users.User>(request.CreateUserDto);
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.CreateUserDto.Password);

        await _userRepository.AddAsync(newUser);
        
        _logger.LogInformation("User created.");

        return _mapper.Map<UserDto>(newUser);
    }
}