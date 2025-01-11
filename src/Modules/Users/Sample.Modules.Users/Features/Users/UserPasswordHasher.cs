using System.Security.Cryptography;
using ErrorOr;

namespace Sample.Modules.Users.Features.Users;

internal static class UserPasswordHasher
{
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    public static ErrorOr<string> Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return UserErrors.InvalidPasswordToHash;

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }
    
    public static ErrorOr<bool> Verify(
        string password,
        string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
            return UserErrors.InvalidPasswordAndHashToVerify;

        var parts = hashedPassword.Split('-');
        var hash = Convert.FromHexString(parts[0]);
        var salt = Convert.FromHexString(parts[1]);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}