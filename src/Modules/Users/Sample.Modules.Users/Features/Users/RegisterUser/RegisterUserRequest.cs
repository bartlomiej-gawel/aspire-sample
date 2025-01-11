using ErrorOr;
using MediatR;

namespace Sample.Modules.Users.Features.Users.RegisterUser;

internal sealed record RegisterUserRequest(
    string Name,
    string Surname,
    string OrganizationName,
    string Email,
    string Phone,
    string Password,
    string RepeatPassword) : IRequest<ErrorOr<Success>>;