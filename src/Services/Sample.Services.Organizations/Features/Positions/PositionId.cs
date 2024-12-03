namespace Sample.Services.Organizations.Features.Positions;

public sealed record PositionId
{
    public Guid Value { get; }

    private PositionId(Guid value) => Value = value;

    public static PositionId Create() => new(Guid.CreateVersion7());
    public static PositionId From(Guid positionId) => new(positionId);

    public static implicit operator PositionId(Guid positionId) => new(positionId);
    public static implicit operator Guid(PositionId positionId) => positionId.Value;
}