using Sample.Services.Organizations.Features.Positions;

namespace Sample.Services.Organizations.Features.Employees;

public sealed class EmployeePosition
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; init; } = null!;
    public Guid PositionId { get; set; }
    public Position Position { get; init; } = null!;
}