using FastEndpoints;
using FluentValidation;

namespace Sample.Modules.Users.Features.Users.LoginUser;

internal sealed class LoginUserRequestValidator : Validator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(255).WithMessage("Email must be at most 255 characters long.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(255).WithMessage("Password must be at most 255 characters long.");
    }
}