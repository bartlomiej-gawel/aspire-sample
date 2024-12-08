using Sample.Services.Organizations.Features.Locations;

namespace Sample.Services.Organizations.Features.Employees;

public sealed class Employee
{
    public Guid Id { get; init; }
    public Guid LocationId { get; private set; }
    public Location Location { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public EmployeeStatus Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Employee()
    {
    }

    private Employee(
        Guid employeeId,
        Guid locationId,
        string employeeName,
        string employeeSurname,
        string employeeEmail,
        string employeePhone)
    {
        Id = employeeId;
        LocationId = locationId;
        Name = employeeName;
        Surname = employeeSurname;
        Email = employeeEmail;
        Phone = employeePhone;
        Status = EmployeeStatus.Inactive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Employee Initialize(
        Guid employeeId,
        Guid locationId,
        string employeeName,
        string employeeSurname,
        string employeeEmail,
        string employeePhone)
    {
        return new Employee(
            employeeId,
            locationId,
            employeeName,
            employeeSurname,
            employeeEmail,
            employeePhone);
    }
}