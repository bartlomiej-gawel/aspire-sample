using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Features.ActivationTokens;
using Sample.Modules.Users.Features.RefreshTokens;
using Sample.Modules.Users.Features.Users;

namespace Sample.Modules.Users.Database;

internal sealed class UsersModuleDbContext : DbContext
{
    public DbSet<ActivationToken> ActivationTokens => Set<ActivationToken>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<User> Users => Set<User>();

    public UsersModuleDbContext(DbContextOptions<UsersModuleDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}