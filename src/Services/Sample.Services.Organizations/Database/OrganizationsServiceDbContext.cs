using Microsoft.EntityFrameworkCore;
using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Database;

public sealed class OrganizationsServiceDbContext : DbContext
{
    public DbSet<Organization> Organizations => Set<Organization>();

    public OrganizationsServiceDbContext(DbContextOptions<OrganizationsServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}