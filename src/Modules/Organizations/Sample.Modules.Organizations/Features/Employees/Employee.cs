using ErrorOr;
using Sample.Modules.Organizations.Features.Locations;

namespace Sample.Modules.Organizations.Features.Employees;

internal sealed class Employee
{
    public Guid Id { get; }
    public Guid LocationId { get; private set; }
    public Location Location { get; init; } = null!;
    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public EmployeeStatus Status { get; private set; }
    public EmployeeRole Role { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Employee()
    {
    }

    private Employee(
        Guid employeeId,
        Guid locationId,
        string name,
        string surname,
        string email,
        string phone)
    {
        Id = employeeId;
        LocationId = locationId;
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        Status = EmployeeStatus.Inactive;
        Role = EmployeeRole.OrganizationAdmin;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Employee InitializeOrganizationAdmin(
        Guid employeeId,
        Guid locationId,
        string name,
        string surname,
        string email,
        string phone)
    {
        return new Employee(
            employeeId,
            locationId,
            name,
            surname,
            email,
            phone);
    }

    public ErrorOr<Success> Activate()
    {
        if (Status == EmployeeStatus.Active)
            return EmployeeErrors.AlreadyActivated;

        Status = EmployeeStatus.Active;
        UpdatedAt = DateTime.UtcNow;

        return Result.Success;
    }
}