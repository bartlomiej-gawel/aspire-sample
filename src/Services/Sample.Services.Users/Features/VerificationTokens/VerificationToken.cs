using Sample.Services.Users.Features.Users;

namespace Sample.Services.Users.Features.VerificationTokens;

public sealed class VerificationToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; init; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }

    private VerificationToken()
    {
    }

    private VerificationToken(
        Guid userId,
        DateTime dateTime)
    {
        Id = Guid.CreateVersion7();
        UserId = userId;
        CreatedAt = dateTime;
        ExpireAt = dateTime.AddDays(1);
    }

    public static VerificationToken Generate(
        Guid userId,
        DateTime dateTime)
    {
        return new VerificationToken(
            userId,
            dateTime);
    }
}