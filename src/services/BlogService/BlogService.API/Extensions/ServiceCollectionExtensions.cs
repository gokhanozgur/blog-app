using System.Text;
using AspNetCoreRateLimit;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Features.Categories.Commands;
using BlogService.Application.Features.Comments.Commands;
using BlogService.Application.Interfaces.BlogPosts;
using BlogService.Application.Interfaces.Categories;
using BlogService.Application.Interfaces.Comments;
using BlogService.Application.Mapping.BlogPosts;
using BlogService.Application.Mapping.Categories;
using BlogService.Application.Mapping.Comments;
using BlogService.Application.Validators.BlogPosts;
using BlogService.Application.Validators.Categories;
using BlogService.Application.Validators.Comments;
using BlogService.Persistence.Contexts;
using BlogService.Persistence.Repositories;
using BlogService.Persistence.Settings;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

namespace BlogService.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(BlogPostProfile).Assembly);
        services.AddAutoMapper(typeof(CommentProfile).Assembly);
        services.AddAutoMapper(typeof(CategoryProfile).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBlogPostCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCommentCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryCommand).Assembly));
        services.AddValidatorsFromAssembly(typeof(CreateBlogPostCommandValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateCommentCommandValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateCategoryCommandValidator).Assembly);
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq", "/", c =>
                {
                    c.Username("admin");
                    c.Password("admin123");
                });
            });
        });
        
        return services;
    }
    
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<MongoDbContext>();
        services.AddHealthChecks()
            .AddMongoDb(
                sp => new MongoDB.Driver.MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value!),
                name: "mongodb"
            );
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
    
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        return services;
    }
    
    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogService API", Version = "v1" });

            // JWT Bearer şeması ekle
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }
    
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Seq(configuration.GetConnectionString("Seq") ?? "http://localhost:5341")
            .WriteTo.GrafanaLoki(configuration.GetConnectionString("Loki") ?? "http://localhost:3100")
            .CreateLogger();

        services.AddSerilog();
        return services;
    }
    
    public static IServiceCollection AddMonitoring(this IServiceCollection services)
    {
        services.AddHealthChecks();
        return services;
    }
    
    public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        return services;
    }
}