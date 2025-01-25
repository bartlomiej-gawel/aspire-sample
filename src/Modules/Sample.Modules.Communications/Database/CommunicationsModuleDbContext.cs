using Microsoft.EntityFrameworkCore;
using Sample.Modules.Communications.Features.Messages;
using Sample.Modules.Communications.Features.Recipients;

namespace Sample.Modules.Communications.Database;

internal sealed class CommunicationsModuleDbContext : DbContext
{
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Recipient> Recipients => Set<Recipient>();

    public CommunicationsModuleDbContext(DbContextOptions<CommunicationsModuleDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}