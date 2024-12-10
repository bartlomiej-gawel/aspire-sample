using Sample.Services.Users.Features.Users;

namespace Sample.Services.Users.Features.RefreshTokens;

public sealed class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; init; } = null!;
    public string Value { get; set; } = null!;
    public DateTime ExpireAt { get; set; }

    private RefreshToken()
    {
    }

    private RefreshToken(
        Guid userId,
        string value)
    {
        Id = Guid.CreateVersion7();
        UserId = userId;
        Value = value;
        ExpireAt = DateTime.UtcNow.AddDays(7);
    }

    public static RefreshToken Create(
        Guid userId,
        string value)
    {
        return new RefreshToken(
            userId,
            value);
    }

    public void Update(string value)
    {
        Value = value;
        ExpireAt = DateTime.UtcNow.AddDays(7);
    }
}