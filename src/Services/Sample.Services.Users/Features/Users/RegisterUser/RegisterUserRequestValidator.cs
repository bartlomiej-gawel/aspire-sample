using FastEndpoints;
using FluentValidation;

namespace Sample.Services.Users.Features.Users.RegisterUser;

public sealed class RegisterUserRequestValidator : Validator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(255);

        RuleFor(x => x.Surname)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(255);

        RuleFor(x => x.OrganizationName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(255);

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MinimumLength(9)
            .MaximumLength(15);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Equal(x => x.RepeatPassword)
            .MinimumLength(8)
            .MaximumLength(255);
    }
}