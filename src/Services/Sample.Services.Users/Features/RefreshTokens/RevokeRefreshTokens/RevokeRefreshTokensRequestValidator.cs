using FastEndpoints;
using FluentValidation;

namespace Sample.Services.Users.Features.RefreshTokens.RevokeRefreshTokens;

public sealed class RevokeRefreshTokensRequestValidator : Validator<RevokeRefreshTokensRequest>
{
    public RevokeRefreshTokensRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}