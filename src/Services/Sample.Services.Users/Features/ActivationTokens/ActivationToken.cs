using Sample.Services.Users.Features.Users;

namespace Sample.Services.Users.Features.ActivationTokens;

public sealed class ActivationToken
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime ExpireAt { get; init; }
}