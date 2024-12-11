using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Features.RefreshTokens;
using Sample.Services.Users.Features.Users;
using Sample.Services.Users.Features.VerificationTokens;

namespace Sample.Services.Users.Database;

public sealed class UsersServiceDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<VerificationToken> VerificationTokens => Set<VerificationToken>();
    
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