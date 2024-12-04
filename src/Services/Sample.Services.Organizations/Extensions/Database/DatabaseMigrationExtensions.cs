using Microsoft.EntityFrameworkCore;

namespace Sample.Services.Organizations.Extensions.Database;

public static class DatabaseMigrationExtensions
{
    public static async Task RunDatabaseMigrationsAsync<TDbContext>(this WebApplication app) 
        where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        var executionStrategy = dbContext.Database.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync();
        });
    }
}