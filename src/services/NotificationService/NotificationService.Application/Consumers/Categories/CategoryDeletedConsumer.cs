using MassTransit;
using SharedEvents.Events.Categories;

namespace NotificationService.Application.Consumers.Categories;

public class CategoryDeletedConsumer: IConsumer<CategoryDeletedEvent>
{
    public async Task Consume(ConsumeContext<CategoryDeletedEvent> context)
    {
        var @event = context.Message;
        
        Console.WriteLine($"Category deleted notification: ({@event.Id})");
            
        await Task.CompletedTask;
    }
}