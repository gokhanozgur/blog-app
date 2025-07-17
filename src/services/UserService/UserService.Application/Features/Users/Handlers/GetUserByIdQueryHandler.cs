using AutoMapper;
using MediatR;
using UserService.Application.DTOs;
using UserService.Application.Features.Users.Queries;
using UserService.Application.Interfaces;

namespace UserService.Application.Features.Users.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new Exception("User not found");

        return _mapper.Map<UserDto>(user);
    }
}