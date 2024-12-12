namespace Sample.Services.Notifications.Features.Notifiers;

public sealed class Notifier
{
    public Guid Id { get; }
    public Guid OrganizationId { get; set; }
    public string OrganizationName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
}