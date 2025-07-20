using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Interfaces;

namespace UserService.Application.Features.Users.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<DeleteUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null) return false;

        await _userRepository.DeleteAsync(user);
        _logger.LogInformation($"User with id {user.Id} has been deleted.");
        
        return true;
    }
}