using ErrorOr;
using MediatR;

namespace Sample.Modules.Users.Features.RefreshTokens.RevokeRefreshTokens;

internal sealed record RevokeRefreshTokensRequest(Guid UserId) : IRequest<ErrorOr<Success>>;