using MassTransit;
using NotificationService.Application.Consumers.BlogPosts;
using NotificationService.Application.Consumers.Categories;
using RabbitMQ.Client;


namespace NotificationService.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BlogPostCreatedConsumer>(c =>
            {
                c.UseMessageRetry(r => r.Immediate(3));
            });
            x.AddConsumer<BlogPostDeletedConsumer>(c =>
            {
                c.UseMessageRetry(r => r.Immediate(3));
            });
            x.AddConsumer<CategoryCreatedConsumer>(c =>
            {
                c.UseMessageRetry(r => r.Immediate(3));
            });
            x.AddConsumer<CategoryDeletedConsumer>(c =>
            {
                c.UseMessageRetry(r => r.Immediate(3));
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseDelayedRedelivery(r =>
                    r.Interval(3, TimeSpan.FromMinutes(5)));
                
                cfg.Host("rabbitmq", "/", c =>
                {
                    c.Username("admin");
                    c.Password("admin123");
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
    
    public static IServiceCollection AddMonitoring(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddRabbitMQ(async sp =>
            {
                var factory = new RabbitMQ.Client.ConnectionFactory
                {
                    Uri = new Uri("amqp://admin:admin123@rabbitmq:5672")
                };
                return await factory.CreateConnectionAsync();
            }, name: "rabbitmq");
        
        return services;
    }
}