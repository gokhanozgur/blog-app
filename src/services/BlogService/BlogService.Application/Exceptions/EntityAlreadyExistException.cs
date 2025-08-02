namespace BlogService.Application.Exceptions;

public class EntityAlreadyExistException : Exception
{
    public EntityAlreadyExistException(string message = "Entity already exist.") : base(message) { }
}