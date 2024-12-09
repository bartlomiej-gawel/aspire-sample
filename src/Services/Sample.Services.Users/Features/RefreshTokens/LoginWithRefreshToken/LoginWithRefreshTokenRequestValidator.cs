using FastEndpoints;
using FluentValidation;

namespace Sample.Services.Users.Features.RefreshTokens.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenRequestValidator : Validator<LoginWithRefreshTokenRequest>
{
    public LoginWithRefreshTokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}