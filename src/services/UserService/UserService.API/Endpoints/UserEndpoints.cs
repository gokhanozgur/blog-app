using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using UserService.Application.DTOs;
using UserService.Application.DTOs.Users;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Features.Users.Queries;

namespace UserService.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (IMediator mediator, LoginUserDto dto) =>
        {
            var token = await mediator.Send(new LoginUserCommand(dto));
            return Results.Ok(new { token });
        });
        
        app.MapGet("/users/me", [Authorize] async (IMediator mediator, ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        });
        
        app.MapPost("/users", [Authorize] async (IMediator mediator, CreateUserDto dto) =>
        {
            var result = await mediator.Send(new CreateUserCommand(dto));
            return Results.Created($"/users/{result.Id}", result);
        });

        app.MapGet("/users/{id}", [Authorize] async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });

        app.MapGet("/users", [Authorize] async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllUsersQuery());
            return Results.Ok(result);
        });

        app.MapPut("/users/{id}", [Authorize] async (IMediator mediator, Guid id, UpdateUserDto dto) =>
        {
            var result = await mediator.Send(new UpdateUserCommand(id, dto));
            return Results.Ok(result);
        });

        app.MapDelete("/users/{id}", [Authorize] async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new DeleteUserCommand(id));
            return result ? Results.NoContent() : Results.NotFound();
        });
    }
}