using Sample.Services.Organizations.Features.Locations;
using Sample.Services.Organizations.Features.Positions;

namespace Sample.Services.Organizations.Features.Organizations;

public sealed class Organization
{
    public OrganizationId Id { get; set; } = null!;
    public ICollection<Location> Locations { get; set; } = [];
    public ICollection<Position> Positions { get; set; } = [];
}