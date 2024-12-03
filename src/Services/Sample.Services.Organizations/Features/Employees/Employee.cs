using Sample.Services.Organizations.Features.Locations;

namespace Sample.Services.Organizations.Features.Employees;

public sealed class Employee
{
    public Guid Id { get; set; }
    public Guid LocationId { get; set; }
    public Location Location { get; init; } = null!;
}