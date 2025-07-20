using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Features.BlogPosts.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace BlogService.API.Endpoints;

public static class BlogEndpoints
{
    public static void MapBlogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/blogposts", [Authorize] async (CreateBlogPostDto dto, IMediator mediator, IValidator<CreateBlogPostDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            
            var result = await mediator.Send(new CreateBlogPostCommand(dto));
            return Results.Created($"/api/blogposts/{result.Id}", result);
        });

        app.MapPut("/api/blogposts/{id}", [Authorize] async (string id, UpdateBlogPostDto dto, IMediator mediator, IValidator<UpdateBlogPostDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            
            var result = await mediator.Send(new UpdateBlogPostCommand(id, dto));
            return Results.Ok(result);
        });

        app.MapDelete("/api/blogposts/{id}", [Authorize] async (string id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteBlogPostCommand { Id = id });
            return result ? Results.NoContent() : Results.NotFound();
        });

        app.MapGet("/api/blogposts/{id}", async (string id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetBlogPostByIdQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        });

        app.MapGet("/api/blogposts", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllBlogPostsQuery());
            return Results.Ok(result);
        });
    }
}