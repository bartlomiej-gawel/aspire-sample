using Sample.Services.Organizations.Features.Positions;

namespace Sample.Services.Organizations.Features.Employees;

public sealed class EmployeePosition
{
    public Guid EmployeeId { get; init; }
    public Employee Employee { get; init; } = null!;
    public Guid PositionId { get; set; }
    public Position Position { get; set; } = null!;

    private EmployeePosition()
    {
    }

    private EmployeePosition(
        Guid employeeId,
        Guid positionId)
    {
        EmployeeId = employeeId;
        PositionId = positionId;
    }

    public static EmployeePosition Initialize(
        Guid employeeId,
        Guid positionId)
    {
        return new EmployeePosition(
            employeeId,
            positionId);
    }
}