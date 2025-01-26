using ErrorOr;
using MediatR;

namespace Sample.Modules.Users.Features.RefreshTokens.RefreshUserAccess;

internal sealed record RefreshUserAccessRequest(string RefreshToken) : IRequest<ErrorOr<RefreshUserAccessResponse>>;