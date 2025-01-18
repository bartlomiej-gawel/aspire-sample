using ErrorOr;
using Sample.Modules.Organizations.Features.Locations;
using Sample.Modules.Organizations.Features.Positions;

namespace Sample.Modules.Organizations.Features.Organizations;

internal sealed class Organization
{
    public Guid OrganizationId { get; }
    public string OrganizationName { get; private set; } = null!;
    public OrganizationStatus Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<Location> Locations { get; init; } = [];
    public ICollection<Position> Positions { get; init; } = [];

    private Organization()
    {
    }

    private Organization(
        Guid organizationId,
        string organizationName)
    {
        OrganizationId = organizationId;
        OrganizationName = organizationName;
        Status = OrganizationStatus.Inactive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Organization Initialize(
        Guid organizationId,
        string organizationName)
    {
        return new Organization(
            organizationId,
            organizationName);
    }

    public ErrorOr<Success> Activate()
    {
        if (Status == OrganizationStatus.Active)
            return OrganizationErrors.AlreadyActivated;

        Status = OrganizationStatus.Active;
        UpdatedAt = DateTime.UtcNow;

        return Result.Success;
    }
}