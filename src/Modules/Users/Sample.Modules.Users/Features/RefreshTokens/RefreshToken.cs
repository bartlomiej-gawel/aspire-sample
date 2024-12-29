using Sample.Modules.Users.Features.Users;

namespace Sample.Modules.Users.Features.RefreshTokens;

internal sealed class RefreshToken
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public User User { get; init; } = null!;
    public string Value { get; private set; } = null!;
    public DateTime ExpireAt { get; private set; }

    private RefreshToken()
    {
    }

    private RefreshToken(
        Guid userId,
        string value,
        DateTime utcDateTime)
    {
        Id = Guid.CreateVersion7();
        UserId = userId;
        Value = value;
        ExpireAt = utcDateTime.AddDays(7);
    }

    public static RefreshToken Create(
        Guid userId,
        string value,
        DateTime utcDateTime)
    {
        return new RefreshToken(
            userId,
            value,
            utcDateTime);
    }

    public void Update(
        string value,
        DateTime utcDateTime)
    {
        Value = value;
        ExpireAt = utcDateTime.AddDays(7);
    }
}