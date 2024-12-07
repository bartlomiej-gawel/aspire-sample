namespace Sample.Services.Users.Features.Users;

public sealed class User
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public Guid OrganizationId { get; } = Guid.CreateVersion7();
    public string OrganizationName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserStatus Status { get; set; }
}