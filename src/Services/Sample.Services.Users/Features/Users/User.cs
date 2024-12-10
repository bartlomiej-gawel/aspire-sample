namespace Sample.Services.Users.Features.Users;

public sealed class User
{
    public Guid Id { get; }
    public Guid OrganizationId { get; }
    public string OrganizationName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserStatus Status { get; set; }

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
    }

    public static User Register(
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
}