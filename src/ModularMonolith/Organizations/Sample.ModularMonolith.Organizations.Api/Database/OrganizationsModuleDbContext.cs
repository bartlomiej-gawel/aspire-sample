using Microsoft.EntityFrameworkCore;
using Sample.ModularMonolith.Organizations.Api.Features.Organizations;

namespace Sample.ModularMonolith.Organizations.Api.Database;

internal sealed class OrganizationsModuleDbContext : DbContext
{
    public DbSet<Organization> Organizations => Set<Organization>();

    public OrganizationsModuleDbContext(DbContextOptions<OrganizationsModuleDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}