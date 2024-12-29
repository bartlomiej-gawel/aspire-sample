namespace Sample.Modules.Notifications.Features.Recipients;

internal sealed class Recipient
{
    public Guid Id { get; }
    public Guid OrganizationId { get; }
    public string OrganizationName { get; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
}