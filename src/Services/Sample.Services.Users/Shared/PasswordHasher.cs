namespace Sample.Services.Users.Shared;

public sealed class PasswordHasher
{
    public string Hash(string password)
    {
        return password;
    }

    public bool Verify(string password, string hashedPassword)
    {
        return password == hashedPassword;
    }
}