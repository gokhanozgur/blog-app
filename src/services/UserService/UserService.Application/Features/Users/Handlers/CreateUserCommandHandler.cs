using AutoMapper;
using MediatR;
using UserService.Application.DTOs;
using UserService.Application.Exceptions;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Interfaces;

namespace UserService.Application.Features.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.CreateUserDto.Email);
        // TODO must check username also.
        if (user == null)
            throw new UserAlreadyExistException();
        
        var newUser = _mapper.Map<UserService.Domain.Entities.Users.User>(request.CreateUserDto);

        await _userRepository.AddAsync(newUser);

        return _mapper.Map<UserDto>(newUser);
    }
}