using NotificationService.API.Extensions;
using Prometheus;
using Shared.Vault;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddVaultConfiguration(
    builder.Configuration,
    $"BlogApp-NotificationService-Application-Settings-{builder.Environment.EnvironmentName}","data");

builder.Services.AddNotificationServiceExtensions(builder.Configuration);
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpMetrics();
app.MapMetrics();
app.MapHealthChecks("/health");

app.Run();