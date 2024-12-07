namespace Sample.Services.Organizations.Features.Locations;

public sealed record LocationAddress
{
    public string Street { get; init; }
    public string City { get; init; }
    public string PostalCode { get; init; }

    private LocationAddress(
        string street,
        string city,
        string postalCode)
    {
        Street = street;
        City = city;
        PostalCode = postalCode;
    }

    public static LocationAddress Initialize()
    {
        return new LocationAddress(
            "Default",
            "Default",
            "Default");
    }
}