using FluentValidation;

namespace Sample.Modules.Users.Features.RefreshTokens.RevokeRefreshTokens;

internal sealed class RevokeRefreshTokensRequestValidator : AbstractValidator<RevokeRefreshTokensRequest>
{
    public RevokeRefreshTokensRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User identifier is required.");
    }
}