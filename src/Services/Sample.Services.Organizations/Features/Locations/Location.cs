using Sample.Services.Organizations.Features.Employees;
using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Features.Locations;

public sealed class Location
{
    public LocationId Id { get; set; } = null!;
    public OrganizationId OrganizationId { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
    public List<Employee> Employees { get; set; } = [];
}