using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Features.Positions;

public sealed class Position
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; init; } = null!;
}