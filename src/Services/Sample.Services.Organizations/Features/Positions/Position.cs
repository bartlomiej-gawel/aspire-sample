using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Features.Positions;

public sealed class Position
{
    public PositionId Id { get; set; } = null!;
    public OrganizationId OrganizationId { get; set; } = null!;
    public Organization Organization { get; init; } = null!;
}