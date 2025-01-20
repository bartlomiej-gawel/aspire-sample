using Sample.Modules.Communications.Api.Features.Recipients;

namespace Sample.Modules.Communications.Api.Features.Messages;

internal sealed class Message
{
    public Guid Id { get; }
    public Guid RecipientId { get; }
    public Recipient Recipient { get; init; } = null!;
    public string Subject { get; set; } = null!;
    public MessageChannel Channel { get; set; }
    public DateTime CreatedAt { get; }
}