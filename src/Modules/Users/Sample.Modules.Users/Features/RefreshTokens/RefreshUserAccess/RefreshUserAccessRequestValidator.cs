using FastEndpoints;
using FluentValidation;

namespace Sample.Modules.Users.Features.RefreshTokens.RefreshUserAccess;

internal sealed class RefreshUserAccessRequestValidator : Validator<RefreshUserAccessRequest>
{
    public RefreshUserAccessRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}