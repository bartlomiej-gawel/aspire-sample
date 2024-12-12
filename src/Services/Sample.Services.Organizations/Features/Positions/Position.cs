using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Features.Positions;

public sealed class Position
{
    public Guid Id { get; init; }
    public Guid OrganizationId { get; init; }
    public Organization Organization { get; init; } = null!;
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Position()
    {
    }

    private Position(
        Guid positionId,
        Guid organizationId)
    {
        Id = positionId;
        OrganizationId = organizationId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Position Initialize(
        Guid positionId,
        Guid organizationId)
    {
        return new Position(
            positionId,
            organizationId);
    }
}