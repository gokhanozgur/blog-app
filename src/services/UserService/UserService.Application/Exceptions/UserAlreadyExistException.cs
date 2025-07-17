namespace UserService.Application.Exceptions;

public class UserAlreadyExistException : Exception
{
    public UserAlreadyExistException(string message = "User alreaedy exist.") : base(message) { }
}