using Sample.Services.Users.Features.Users;

namespace Sample.Services.Users.Features.ActivationTokens;

public sealed class ActivationToken
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public User User { get; init; } = null!;
    public DateTime CreatedAt { get; }
    public DateTime ExpireAt { get; }

    private ActivationToken()
    {
    }

    private ActivationToken(Guid userId)
    {
        Id = Guid.CreateVersion7();
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        ExpireAt = DateTime.UtcNow.AddDays(1);
    }

    public static ActivationToken Generate(Guid userId)
    {
        return new ActivationToken(userId);
    }
}