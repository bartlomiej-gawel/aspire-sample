using FastEndpoints;
using FluentValidation;

namespace Sample.Services.Users.Features.VerificationTokens.VerifyUserRegistration;

public sealed class VerifyUserRegistrationRequestValidator : Validator<VerifyUserRegistrationRequest>
{
    public VerifyUserRegistrationRequestValidator()
    {
        RuleFor(x => x.VerificationToken)
            .NotEmpty();
    }
}