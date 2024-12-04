using Sample.Services.Organizations.Features.Employees;
using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Features.Locations;

public sealed class Location
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; init; } = null!;
    public string Name { get; set; } = null!;
    public LocationAddress Address { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Employee> Employees { get; set; } = [];
}