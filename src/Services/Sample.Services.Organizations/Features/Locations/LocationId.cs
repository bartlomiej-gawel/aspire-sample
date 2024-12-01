namespace Sample.Services.Organizations.Features.Locations;

public sealed record LocationId
{
    public Guid Value { get; }

    private LocationId(Guid value) => Value = value;

    public static LocationId Create() => new(Guid.CreateVersion7());
    public static LocationId From(Guid locationId) => new(locationId);

    public static implicit operator LocationId(Guid locationId) => new(locationId);
    public static implicit operator Guid(LocationId locationId) => locationId.Value;
}