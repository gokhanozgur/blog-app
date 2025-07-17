namespace UserService.Application.DTOs.Users;

public class LoginUserDto
{
    public string Email { get; set; } = null!;
    public string Username { get; set; }
    public string Password { get; set; } = null!;
}