using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Notifications.Features.Recipients;

namespace Sample.Services.Notifications.Database;

public sealed class NotificationsServiceDbContext : DbContext
{
    public DbSet<Recipient> Recipients => Set<Recipient>();

    public NotificationsServiceDbContext(DbContextOptions<NotificationsServiceDbContext> options)
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