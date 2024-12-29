using FastEndpoints;
using FluentValidation;

namespace Sample.Modules.Users.Features.RefreshTokens.RevokeRefreshTokens;

internal sealed class RevokeRefreshTokensRequestValidator : Validator<RevokeRefreshTokensRequest>
{
    public RevokeRefreshTokensRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User identifier is required.");
    }
}