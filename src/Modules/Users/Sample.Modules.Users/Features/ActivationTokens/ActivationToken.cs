using Sample.Modules.Users.Features.Users;

namespace Sample.Modules.Users.Features.ActivationTokens;

internal sealed class ActivationToken
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public User User { get; init; } = null!;
    public DateTime CreatedAt { get; }
    public DateTime ExpireAt { get; }

    private ActivationToken()
    {
    }

    private ActivationToken(
        Guid userId,
        DateTime utcDateTime)
    {
        Id = Guid.CreateVersion7();
        UserId = userId;
        CreatedAt = utcDateTime;
        ExpireAt = utcDateTime.AddDays(1);
    }

    public static ActivationToken Create(
        Guid userId,
        DateTime utcDateTime)
    {
        return new ActivationToken(
            userId,
            utcDateTime);
    }
}