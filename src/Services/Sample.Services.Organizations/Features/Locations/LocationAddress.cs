namespace Sample.Services.Organizations.Features.Locations;

public sealed record LocationAddress
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }

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
    
    public void Update(
        string street,
        string city,
        string postalCode)
    {
        Street = street;
        City = city;
        PostalCode = postalCode;
    }
}