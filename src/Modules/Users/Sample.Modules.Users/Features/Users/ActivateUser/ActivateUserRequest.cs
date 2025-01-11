using ErrorOr;
using MediatR;

namespace Sample.Modules.Users.Features.Users.ActivateUser;

internal sealed record ActivateUserRequest(Guid ActivationToken) : IRequest<ErrorOr<Success>>;