using ErrorOr;

namespace Sample.Modules.Users.Features.Users;

internal sealed class User
{
    public Guid Id { get; }
    public Guid OrganizationId { get; }
    public string OrganizationName { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public UserStatus Status { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private User()
    {
    }

    private User(
        string organizationName,
        string name,
        string surname,
        string email,
        string phone,
        string password)
    {
        Id = Guid.CreateVersion7();
        OrganizationId = Guid.CreateVersion7();
        OrganizationName = organizationName;
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        Password = password;
        Status = UserStatus.Inactive;
        Role = UserRole.OrganizationAdmin;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static User RegisterOrganizationAdmin(
        string organizationName,
        string name,
        string surname,
        string email,
        string phone,
        string password)
    {
        return new User(
            organizationName,
            name,
            surname,
            email,
            phone,
            password);
    }

    public ErrorOr<Success> Activate()
    {
        if (Status == UserStatus.Active)
            return UserErrors.AlreadyActive;
        
        Status = UserStatus.Active;
        UpdatedAt = DateTime.UtcNow;
        
        return Result.Success;
    }
}