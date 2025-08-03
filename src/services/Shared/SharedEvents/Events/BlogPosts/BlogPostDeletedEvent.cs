namespace SharedEvents.Events.BlogPosts;

public class BlogPostDeletedEvent
{
    public string BlogPostId { get; set; } = null!;
}