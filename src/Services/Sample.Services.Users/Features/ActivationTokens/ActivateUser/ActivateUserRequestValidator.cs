using FastEndpoints;
using FluentValidation;

namespace Sample.Services.Users.Features.ActivationTokens.ActivateUser;

public sealed class ActivateUserRequestValidator : Validator<ActivateUserRequest>
{
    public ActivateUserRequestValidator()
    {
        RuleFor(x => x.ActivationToken)
            .NotEmpty();
    }
}