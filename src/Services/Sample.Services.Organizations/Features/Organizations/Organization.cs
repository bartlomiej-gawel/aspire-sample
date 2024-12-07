using Sample.Services.Organizations.Features.Locations;
using Sample.Services.Organizations.Features.Positions;

namespace Sample.Services.Organizations.Features.Organizations;

public sealed class Organization
{
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public OrganizationStatus Status { get; set; }
    public ICollection<Location> Locations { get; set; } = [];
    public ICollection<Position> Positions { get; set; } = [];
}