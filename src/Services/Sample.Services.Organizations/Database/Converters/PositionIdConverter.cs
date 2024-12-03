using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sample.Services.Organizations.Features.Positions;

namespace Sample.Services.Organizations.Database.Converters;

public sealed class PositionIdConverter : ValueConverter<PositionId, Guid>
{
    public PositionIdConverter() : base(
        positionId => positionId.Value,
        positionId => PositionId.From(positionId))
    {
    }
}