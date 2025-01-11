using FluentValidation;

namespace Sample.Modules.Users.Features.Users.ActivateUser;

internal sealed class ActivateUserRequestValidator : AbstractValidator<ActivateUserRequest>
{
    public ActivateUserRequestValidator()
    {
        RuleFor(x => x.ActivationToken)
            .NotEmpty().WithMessage("Activation token is required.");
    }
}