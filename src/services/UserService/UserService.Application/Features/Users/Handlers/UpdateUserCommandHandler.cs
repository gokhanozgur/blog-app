using AutoMapper;
using MediatR;
using UserService.Application.DTOs;
using UserService.Application.DTOs.Users;
using UserService.Application.Exceptions;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Interfaces;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Users.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UpdateUserDto.UserId);
        if (user == null)
            throw new UserNotFoundException();
        
        user.UserName = request.UpdateUserDto.UserName ?? user.UserName;
        user.Email = request.UpdateUserDto.Email ?? user.Email;
        user.Role = Enum.TryParse(request.UpdateUserDto.Role, out UserRole role) ? role : user.Role;
        user.IsActive = request.UpdateUserDto.IsActive ?? user.IsActive;

        await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserDto>(user);
    }
}