using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Sample.Services.Organizations.Database;

namespace Sample.Services.Organizations.Extensions;

public static class DatabaseExtensions
{
    public static async Task ConfigureDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrganizationsServiceDbContext>();
        
        await CreateDatabaseAsync(dbContext);
        await RunMigrationsAsync(dbContext);
    }
    
    private static async Task CreateDatabaseAsync(OrganizationsServiceDbContext dbContext)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
        
        var executionStrategy = dbContext.Database.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync()) 
                await dbCreator.CreateAsync();
        });
    }

    private static async Task RunMigrationsAsync(OrganizationsServiceDbContext dbContext)
    {
        var executionStrategy = dbContext.Database.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            await dbContext.Database.MigrateAsync();
            await transaction.CommitAsync();
        });
    }
}