using ErrorOr;

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

    public static ErrorOr<LocationAddress> CreateLocationAddress(
        string street,
        string city,
        string zipCode)
    {
        return new LocationAddress(
            street,
            city,
            zipCode);
    }
}