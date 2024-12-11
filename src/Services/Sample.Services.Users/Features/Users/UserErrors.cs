using ErrorOr;

namespace Sample.Services.Users.Features.Users;

public static class UserErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "UserErrors.NotFound",
        "User with provided id was not found");
    
    public static readonly Error AlreadyActivated = Error.Conflict(
        "UserErrors.AlreadyActivated",
        "User with provided id is already activated.");
    
    public static readonly Error NotActivated = Error.Conflict(
        "UserErrors.NotActivated",
        "User with provided id is not activated");
    
    public static readonly Error OrganizationNameAlreadyExists = Error.Conflict(
        "UserErrors.OrganizationNameAlreadyExists",
        "Organization with provided name already exists");
    
    public static readonly Error EmailAlreadyExists = Error.Conflict(
        "UserErrors.EmailAlreadyExists",
        "User with provided email already exists");
    
    public static readonly Error IncorrectPassword = Error.Conflict(
        "UserErrors.IncorrectPassword",
        "Provided password is not correct");
}