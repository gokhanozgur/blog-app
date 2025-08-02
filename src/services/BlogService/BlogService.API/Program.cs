using AspNetCoreRateLimit;
using BlogService.API.Endpoints;
using BlogService.API.Extensions;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerWithJwt();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
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

app.UseHttpsRedirection();
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.MapBlogEndpoints();
app.MapCategoryEndpoints();
app.MapCommentEndpoints();
app.MapHealthChecks("/health");
app.UseHttpMetrics();
app.MapMetrics();

app.Run();