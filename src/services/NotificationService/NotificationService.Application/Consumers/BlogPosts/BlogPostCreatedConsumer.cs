using MassTransit;
using SharedEvents.Events.BlogPosts;

namespace NotificationService.Application.Consumers.BlogPosts;

public class BlogPostCreatedConsumer : IConsumer<BlogPostCreatedEvent>
{
    public async Task Consume(ConsumeContext<BlogPostCreatedEvent> context)
    {
        var @event = context.Message;
        
        Console.WriteLine($"New blog post created notification: {@event.Title} ({@event.BlogPostId})");
            
        // If you want sent e-mail, SMS, push notification
        // await SendEmailNotification(@event);
        // await SendPushNotification(@event);
            
        await Task.CompletedTask;
    }
}