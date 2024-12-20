using FluentValidation;

namespace Sample.Services.Users.Features.RefreshTokens.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenRequestValidator : AbstractValidator<LoginWithRefreshTokenRequest>
{
    public LoginWithRefreshTokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}