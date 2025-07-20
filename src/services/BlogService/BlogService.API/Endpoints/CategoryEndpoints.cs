using BlogService.Application.DTOs.Categories;
using BlogService.Application.Features.Categories.Commands;
using BlogService.Application.Features.Categories.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace BlogService.API.Endpoints;

public static class CategoryEndpoints
{
    private static readonly string GroupTag = "Category";
    private static readonly string Version = "v1";
    
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/category", [Authorize] async (CreateCategoryDto dto, IMediator mediator, IValidator<CreateCategoryDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            
            var result = await mediator.Send(new CreateCategoryCommand(dto));
            return Results.Created($"/api/category/{result.Id}", result);
        })
        .WithTags(GroupTag)
        .WithGroupName(Version);

        app.MapPut("/api/category/{id}", [Authorize] async (string id, UpdateCategoryDto dto, IMediator mediator, IValidator<UpdateCategoryDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            
            var result = await mediator.Send(new UpdateCategoryCommand(id, dto));
            return Results.Ok(result);
        })
        .WithTags(GroupTag)
        .WithGroupName(Version);

        app.MapDelete("/api/category/{id}", [Authorize] async (string id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteCategoryCommand { Id = id });
            return result ? Results.NoContent() : Results.NotFound();
        })
        .WithTags(GroupTag)
        .WithGroupName(Version);

        app.MapGet("/api/category/{id}", async (string categoryId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCategoryByIdQuery { CategoryId = categoryId });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithTags(GroupTag)
        .WithGroupName(Version);

        app.MapGet("/api/category", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllCategoriesQuery());
            return Results.Ok(result);
        })
        .WithTags(GroupTag)
        .WithGroupName(Version);
    }
}