using Sample.Modules.Organizations.Features.Employees;
using Sample.Modules.Organizations.Features.Positions;

namespace Sample.Modules.Organizations.Features.EmployeePositions;

internal sealed class EmployeePosition
{
    public Guid EmployeeId { get; }
    public Employee Employee { get; init; } = null!;
    public Guid PositionId { get; }
    public Position Position { get; init; } = null!;

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