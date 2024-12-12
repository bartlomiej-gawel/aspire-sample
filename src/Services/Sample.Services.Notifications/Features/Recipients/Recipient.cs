namespace Sample.Services.Notifications.Features.Recipients;

public sealed class Recipient
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

    private Recipient()
    {
    }

    private Recipient(
        Guid notifierId,
        Guid organizationId,
        string organizationName,
        string notifierName,
        string notifierSurname,
        string notifierEmail,
        string notifierPhone)
    {
        Id = notifierId;
        OrganizationId = organizationId;
        OrganizationName = organizationName;
        Name = notifierName;
        Surname = notifierSurname;
        Email = notifierEmail;
        Phone = notifierPhone;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Recipient Initialize(
        Guid notifierId,
        Guid organizationId,
        string organizationName,
        string notifierName,
        string notifierSurname,
        string notifierEmail,
        string notifierPhone)
    {
        return new Recipient(
            notifierId,
            organizationId,
            organizationName,
            notifierName,
            notifierSurname,
            notifierEmail,
            notifierPhone);
    }
}