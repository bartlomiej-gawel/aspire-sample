using Microsoft.EntityFrameworkCore;
using Sample.Modules.Notifications.Features.Recipients;

namespace Sample.Modules.Notifications.Database;

internal sealed class NotificationsModuleDbContext : DbContext
{
    public DbSet<Recipient> Recipients => Set<Recipient>();
    
    public NotificationsModuleDbContext(DbContextOptions<NotificationsModuleDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}