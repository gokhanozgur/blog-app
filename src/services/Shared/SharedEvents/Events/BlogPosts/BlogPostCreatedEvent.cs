namespace SharedEvents.Events.BlogPosts;

public class BlogPostCreatedEvent
{
    public string BlogPostId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string AuthorId { get; set; }  = null!;
    public DateTime CreatedAt { get; set; }
}