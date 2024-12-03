using Sample.Services.Organizations.Features.Positions;

namespace Sample.Services.Organizations.Features.Employees;

public sealed class EmployeePosition
{
    public EmployeeId EmployeeId { get; set; } = null!;
    public Employee Employee { get; init; } = null!;
    public PositionId PositionId { get; set; } = null!;
    public Position Position { get; init; } = null!;
}