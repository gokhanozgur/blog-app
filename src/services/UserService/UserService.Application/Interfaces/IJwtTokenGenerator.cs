namespace UserService.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string userName, string email, string role);
}