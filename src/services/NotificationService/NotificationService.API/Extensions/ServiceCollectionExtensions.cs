using MassTransit;
using NotificationService.Application.Consumers.BlogPosts;
using NotificationService.Application.Consumers.Categories;


namespace NotificationService.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BlogPostCreatedConsumer>();
            x.AddConsumer<BlogPostDeletedConsumer>();
            x.AddConsumer<CategoryCreatedConsumer>();
            x.AddConsumer<CategoryDeletedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", c =>
                {
                    c.Username("admin");
                    c.Password("admin123");
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}