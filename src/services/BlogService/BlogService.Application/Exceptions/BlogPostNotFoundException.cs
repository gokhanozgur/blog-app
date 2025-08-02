namespace BlogService.Application.Exceptions;

public class BlogPostNotFoundException : Exception
{
    public BlogPostNotFoundException(string message = "Blog post not found.") : base(message) { }
}