using Microsoft.EntityFrameworkCore;

namespace Sample.Services.Users.Database;

public sealed class UsersServiceDbContext : DbContext
{
    public UsersServiceDbContext(DbContextOptions<UsersServiceDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}