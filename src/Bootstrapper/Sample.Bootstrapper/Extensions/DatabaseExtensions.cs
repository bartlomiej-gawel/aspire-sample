namespace Sample.Bootstrapper.Extensions;

internal static class DatabaseExtensions
{
    // public static async Task RunDatabaseMigrationsAsync<TDbContext>(this WebApplication app) 
    //     where TDbContext : DbContext
    // {
    //     using var scope = app.Services.CreateScope();
    //     var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
    //     
    //     var executionStrategy = dbContext.Database.CreateExecutionStrategy();
    //     await executionStrategy.ExecuteAsync(async () =>
    //     {
    //         await dbContext.Database.MigrateAsync();
    //     });
    // }
}