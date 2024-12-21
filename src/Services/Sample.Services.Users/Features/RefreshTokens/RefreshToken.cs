using Sample.Services.Users.Features.Users;

namespace Sample.Services.Users.Features.RefreshTokens;

public sealed class RefreshToken
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
    public string Value { get; set; } = null!;
    public DateTime ExpireAt { get; set; }
}