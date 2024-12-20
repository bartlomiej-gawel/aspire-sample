using FluentValidation;

namespace Sample.Services.Users.Features.Users.RegisterUser;

public sealed class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(255).WithMessage("Name must be at most 255 characters long.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MinimumLength(3).WithMessage("Surname must be at least 3 characters long.")
            .MaximumLength(255).WithMessage("Surname must be at most 255 characters long.");

        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("Organization name is required.")
            .MinimumLength(3).WithMessage("Organization name must be at least 3 characters long.")
            .MaximumLength(255).WithMessage("Organization name must be at most 255 characters long.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(255).WithMessage("Email must be at most 255 characters long.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required.")
            .MinimumLength(9).WithMessage("Phone must be at least 9 characters long.")
            .MaximumLength(15).WithMessage("Phone must be at most 15 characters long.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Equal(x => x.RepeatPassword).WithMessage("Passwords do not match.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(255).WithMessage("Password must be at most 255 characters long.");
    }
}