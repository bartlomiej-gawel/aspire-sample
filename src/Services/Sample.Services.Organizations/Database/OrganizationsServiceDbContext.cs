using Microsoft.EntityFrameworkCore;
using Sample.Services.Organizations.Features.Employees;
using Sample.Services.Organizations.Features.Locations;
using Sample.Services.Organizations.Features.Organizations;
using Sample.Services.Organizations.Features.Positions;

namespace Sample.Services.Organizations.Database;

public sealed class OrganizationsServiceDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeePosition> EmployeePositions => Set<EmployeePosition>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Position> Positions => Set<Position>();

    public OrganizationsServiceDbContext(DbContextOptions<OrganizationsServiceDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}