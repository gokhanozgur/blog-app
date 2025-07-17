using MediatR;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Interfaces;

namespace UserService.Application.Features.Users.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null) return false;

        await _userRepository.DeleteAsync(user);
        return true;
    }
}