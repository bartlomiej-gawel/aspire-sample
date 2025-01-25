using Sample.Modules.Communications.Features.Recipients;

namespace Sample.Modules.Communications.Features.Messages;

internal sealed class Message
{
    public Guid Id { get; }
    public Guid RecipientId { get; }
    public Recipient Recipient { get; init; } = null!;
    public string Subject { get; set; } = null!;
    public MessageChannel Channel { get; set; }
    public DateTime CreatedAt { get; }

    private Message()
    {
    }

    private Message(
        Guid recipientId,
        string subject,
        MessageChannel channel)
    {
        Id = Guid.CreateVersion7();
        RecipientId = recipientId;
        Subject = subject;
        Channel = channel;
        CreatedAt = DateTime.UtcNow;
    }

    public static Message Create(
        Guid recipientId,
        string subject,
        MessageChannel channel)
    {
        return new Message(
            recipientId,
            subject,
            channel);
    }
}