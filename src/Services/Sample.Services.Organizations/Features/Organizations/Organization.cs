using Sample.Services.Organizations.Features.Locations;

namespace Sample.Services.Organizations.Features.Organizations;

public sealed class Organization
{
    public OrganizationId Id { get; set; } = null!;
    public List<Location> Locations { get; set; } = [];
}