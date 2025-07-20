using BlogService.Application.DTOs.Comments;
using BlogService.Application.Features.Comments.Commands;
using BlogService.Application.Features.Comments.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace BlogService.API.Endpoints;

public static class CommentEndpoints
{
    public static void MapCommentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/comments",  [Authorize] async (CreateCommentDto dto, IMediator mediator, IValidator<CreateCommentDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            
            var result = await mediator.Send(new CreateCommentCommand(dto));
            return Results.Created($"/api/comments/{result.Id}", result);
        });
        
        app.MapPut("/api/comments/{id}", [Authorize] async (string id, UpdateCommentDto dto, IMediator mediator, IValidator<UpdateCommentDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            
            var result = await mediator.Send(new UpdateCommentCommand(id, dto));
            return Results.Ok(result);
        });
        
        app.MapDelete("/api/comments/{id}",  [Authorize] async (string id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteCommentCommand() { Id = id });
            return result ? Results.NoContent() : Results.NotFound();
        });
        
        app.MapGet("/api/comments/{BlogPostId}", async (string BlogPostId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCommentByBlogPostIdQuery() { BlogPostId = BlogPostId });
            return result != null ? Results.Ok(result) : Results.NotFound();
        });
    }
}