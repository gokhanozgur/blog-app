using MassTransit;
using SharedEvents.Events.BlogPosts;
using SharedEvents.Events.Categories;

namespace NotificationService.Application.Consumers.Categories;

public class CategoryCreatedConsumer : IConsumer<CategoryCreatedEvent>
{
    public async Task Consume(ConsumeContext<CategoryCreatedEvent> context)
    {
        var @event = context.Message;
        
        Console.WriteLine($"New category created notification: {@event.Name} ({@event.Id})");
            
        // If you want sent e-mail, SMS, push notification
        // await SendEmailNotification(@event);
        // await SendPushNotification(@event);
            
        await Task.CompletedTask;
    }
}