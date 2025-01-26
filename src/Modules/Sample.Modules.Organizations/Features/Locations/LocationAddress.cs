namespace Sample.Modules.Organizations.Features.Locations;

internal sealed record LocationAddress
{
    public string Street { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
}