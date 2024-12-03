using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sample.Services.Organizations.Features.Locations;

namespace Sample.Services.Organizations.Database.Converters;

public sealed class LocationIdConverter : ValueConverter<LocationId, Guid>
{
    public LocationIdConverter() : base(
        locationId => locationId.Value,
        locationId => LocationId.From(locationId))
    {
    }
}