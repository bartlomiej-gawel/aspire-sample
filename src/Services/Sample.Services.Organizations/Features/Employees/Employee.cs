using Sample.Services.Organizations.Features.Locations;

namespace Sample.Services.Organizations.Features.Employees;

public sealed class Employee
{
    public EmployeeId Id { get; set; } = null!;
    public LocationId LocationId { get; set; } = null!;
    public Location Location { get; init; } = null!;
}