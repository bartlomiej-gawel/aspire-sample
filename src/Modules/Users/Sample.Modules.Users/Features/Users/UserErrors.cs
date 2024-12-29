using ErrorOr;

namespace Sample.Modules.Users.Features.Users;

internal static class UserErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "UserErrors.NotFound",
        "User with provided id was not found.");

    public static readonly Error NotActivated = Error.Conflict(
        "UserErrors.NotActivated",
        "User is not activated.");

    public static readonly Error AlreadyActive = Error.Conflict(
        "UserErrors.AlreadyActive",
        "User is already active.");

    public static readonly Error OrganizationNameAlreadyExists = Error.Conflict(
        "UserErrors.OrganizationNameAlreadyExists",
        "Organization name already exists.");

    public static readonly Error EmailAlreadyInUse = Error.Conflict(
        "UserErrors.EmailAlreadyInUse",
        "Email already in use.");

    public static readonly Error InvalidPasswordToHash = Error.Conflict(
        "UserErrors.PasswordHashingError",
        "Invalid password to hash.");

    public static readonly Error InvalidPasswordAndHashToVerify = Error.Conflict(
        "UserErrors.PasswordVerificationError",
        "Invalid password and hash to verify.");
}