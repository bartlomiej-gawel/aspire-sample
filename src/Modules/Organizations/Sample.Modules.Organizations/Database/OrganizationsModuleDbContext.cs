using Microsoft.EntityFrameworkCore;
using Sample.Modules.Organizations.Features.EmployeePositions;
using Sample.Modules.Organizations.Features.Employees;
using Sample.Modules.Organizations.Features.Locations;
using Sample.Modules.Organizations.Features.Organizations;
using Sample.Modules.Organizations.Features.Positions;

namespace Sample.Modules.Organizations.Database;

internal sealed class OrganizationsModuleDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeePosition> EmployeePositions => Set<EmployeePosition>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Position> Positions => Set<Position>();

    public OrganizationsModuleDbContext(DbContextOptions<OrganizationsModuleDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}