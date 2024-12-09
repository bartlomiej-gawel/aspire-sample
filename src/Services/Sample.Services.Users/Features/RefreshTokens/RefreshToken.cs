using Sample.Services.Users.Features.Users;

namespace Sample.Services.Users.Features.RefreshTokens;

public sealed class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Value { get; set; } = null!;
    public DateTime ExpireAt { get; set; }
}