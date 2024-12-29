using Sample.Modules.Organizations.Features.Employees;
using Sample.Modules.Organizations.Features.Organizations;

namespace Sample.Modules.Organizations.Features.Locations;

internal sealed class Location
{
    public Guid Id { get; init; }
    public Guid OrganizationId { get; init; }
    public Organization Organization { get; init; } = null!;
    public string Name { get; set; } = null!;
    public LocationAddress Address { get; set; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Employee> Employees { get; init; } = [];
    
    private Location()
    {
    }
    
    private Location(
        Guid locationId,
        Guid organizationId)
    {
        Id = locationId;
        OrganizationId = organizationId;
        Name = "Default";
        Address = LocationAddress.Initialize();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }
    
    public static Location Initialize(
        Guid locationId,
        Guid organizationId)
    {
        return new Location(
            locationId,
            organizationId);
    }
    
    public void Update(
        string name,
        string street,
        string city,
        string postalCode)
    {
        Name = name;
        Address.Update(
            street,
            city,
            postalCode);
        UpdatedAt = DateTime.UtcNow;
    }
}