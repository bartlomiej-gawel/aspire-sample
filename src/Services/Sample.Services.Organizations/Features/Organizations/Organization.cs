using ErrorOr;
using Sample.Services.Organizations.Features.Locations;
using Sample.Services.Organizations.Features.Positions;

namespace Sample.Services.Organizations.Features.Organizations;

public sealed class Organization
{
    public Guid Id { get; }
    public string Name { get; private set; } = null!;
    public OrganizationStatus Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<Location> Locations { get; set; } = [];
    public ICollection<Position> Positions { get; set; } = [];

    private Organization()
    {
    }

    private Organization(
        Guid organizationId,
        string organizationName)
    {
        Id = organizationId;
        Name = organizationName;
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