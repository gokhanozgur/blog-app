namespace UserService.Application.DTOs;

public class CreateUserDto
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
}