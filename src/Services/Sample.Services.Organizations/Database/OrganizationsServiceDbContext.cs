using Microsoft.EntityFrameworkCore;
using Sample.Services.Organizations.Features.Employees;
using Sample.Services.Organizations.Features.Locations;
using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Database;

public sealed class OrganizationsServiceDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Organization> Organizations => Set<Organization>();

    public OrganizationsServiceDbContext(DbContextOptions<OrganizationsServiceDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}