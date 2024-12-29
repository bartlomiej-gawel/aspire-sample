namespace Sample.Services.Organizations.Features.Locations;

public sealed record LocationAddress
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}