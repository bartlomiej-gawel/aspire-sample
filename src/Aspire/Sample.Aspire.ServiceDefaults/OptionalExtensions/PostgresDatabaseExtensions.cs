using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Aspire.ServiceDefaults.OptionalExtensions;

public static class PostgresDatabaseExtensions
{
    public static async Task ConfigureDatabaseAsync<TDbContext>(this WebApplication app) 
        where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        await CreateDatabaseAsync(dbContext);
        await RunMigrationsAsync(dbContext);
    }
    
    private static async Task CreateDatabaseAsync<TDbContext>(TDbContext dbContext) 
        where TDbContext : DbContext
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
        
        var executionStrategy = dbContext.Database.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync()) 
                await dbCreator.CreateAsync();
        });
    }

    private static async Task RunMigrationsAsync<TDbContext>(TDbContext dbContext) 
        where TDbContext : DbContext
    {
        var executionStrategy = dbContext.Database.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync();
        });
    }
}