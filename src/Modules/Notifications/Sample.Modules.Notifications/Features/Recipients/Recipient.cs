namespace Sample.Modules.Notifications.Features.Recipients;

internal sealed class Recipient
{
    public Guid Id { get; private set; }
    public Guid OrganizationId { get; private set; }
    public string OrganizationName { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Recipient()
    {
    }

    private Recipient(
        Guid id,
        Guid organizationId,
        string organizationName,
        string name,
        string surname,
        string email,
        string phone)
    {
        Id = id;
        OrganizationId = organizationId;
        OrganizationName = organizationName;
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Recipient Create(
        Guid id,
        Guid organizationId,
        string organizationName,
        string name,
        string surname,
        string email,
        string phone)
    {
        return new Recipient(
            id,
            organizationId,
            organizationName,
            name,
            surname,
            email,
            phone);
    }
}