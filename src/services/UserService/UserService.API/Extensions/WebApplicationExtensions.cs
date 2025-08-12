using Microsoft.EntityFrameworkCore;
using UserService.Persistence.Contexts;

namespace UserService.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        await db.Database.MigrateAsync();
        return app;
    }
}