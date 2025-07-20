using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using UserService.API.Endpoints;
using UserService.API.Extensions;
using UserService.API.Middlewares;
using UserService.Application.Interfaces;
using UserService.Persistence.Contexts;
using UserService.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerWithJwt();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSerilogLogging(builder.Configuration);
builder.Services.AddMonitoring();
builder.Services.AddRateLimiting(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapUserEndpoints();
app.MapHealthChecks("/health");
app.UseHttpMetrics();
app.MapMetrics();

app.Run();
