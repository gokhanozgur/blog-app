namespace BlogService.Application.Exceptions;

public class BlogPostAlreadyExistException : Exception
{
    public BlogPostAlreadyExistException(string message = "Blog post alreaedy exist.") : base(message) { }
}