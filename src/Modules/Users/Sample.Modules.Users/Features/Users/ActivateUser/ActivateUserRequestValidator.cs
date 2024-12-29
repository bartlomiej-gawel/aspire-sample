using FastEndpoints;
using FluentValidation;

namespace Sample.Modules.Users.Features.Users.ActivateUser;

internal sealed class ActivateUserRequestValidator : Validator<ActivateUserRequest>
{
    public ActivateUserRequestValidator()
    {
        RuleFor(x => x.ActivationToken)
            .NotEmpty().WithMessage("Activation token is required.");
    }
}