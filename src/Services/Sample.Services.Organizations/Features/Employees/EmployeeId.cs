namespace Sample.Services.Organizations.Features.Employees;

public sealed record EmployeeId
{
    public Guid Value { get; }

    private EmployeeId(Guid value) => Value = value;

    public static EmployeeId Create() => new(Guid.CreateVersion7());
    public static EmployeeId From(Guid employeeId) => new(employeeId);

    public static implicit operator EmployeeId(Guid employeeId) => new(employeeId);
    public static implicit operator Guid(EmployeeId employeeId) => employeeId.Value;
}