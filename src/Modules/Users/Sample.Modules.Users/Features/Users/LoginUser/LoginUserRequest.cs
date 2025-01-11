using ErrorOr;
using MediatR;

namespace Sample.Modules.Users.Features.Users.LoginUser;

internal sealed record LoginUserRequest(
    string Email,
    string Password) : IRequest<ErrorOr<LoginUserResponse>>;