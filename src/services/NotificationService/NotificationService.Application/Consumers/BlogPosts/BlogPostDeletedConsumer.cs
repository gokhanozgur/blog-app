using MassTransit;
using SharedEvents.Events.BlogPosts;

namespace NotificationService.Application.Consumers.BlogPosts;

public class BlogPostDeletedConsumer : IConsumer<BlogPostDeletedEvent>
{
    public async Task Consume(ConsumeContext<BlogPostDeletedEvent> context)
    {
        var @event = context.Message;
        
        Console.WriteLine($"Blog post deleted notification: ({@event.BlogPostId})");
            
        await Task.CompletedTask;
    }
}