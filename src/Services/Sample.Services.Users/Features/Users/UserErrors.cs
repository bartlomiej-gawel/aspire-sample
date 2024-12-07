using ErrorOr;

namespace Sample.Services.Users.Features.Users;

public static class UserErrors
{
    public static readonly Error OrganizationNameAlreadyExists = Error.Conflict(
        "UserErrors.OrganizationNameAlreadyExists",
        "Organization with provided name already exists");
    
    public static readonly Error EmailAlreadyExists = Error.Conflict(
        "UserErrors.EmailAlreadyExists",
        "User with provided email already exists");
}