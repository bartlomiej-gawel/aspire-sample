using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Features.ActivationTokens;
using Sample.Services.Users.Features.RefreshTokens;
using Sample.Services.Users.Features.Users;

namespace Sample.Services.Users.Database;

public sealed class UsersServiceDbContext : DbContext
{
    public DbSet<ActivationToken> ActivationTokens => Set<ActivationToken>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<User> Users => Set<User>();
    
    public UsersServiceDbContext(DbContextOptions<UsersServiceDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}