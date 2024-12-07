using Sample.Services.Organizations.Features.Locations;

namespace Sample.Services.Organizations.Features.Employees;

public sealed class Employee
{
    public Guid Id { get; set; }
    public Guid LocationId { get; set; }
    public Location Location { get; init; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public EmployeeStatus Status { get; set; }
}