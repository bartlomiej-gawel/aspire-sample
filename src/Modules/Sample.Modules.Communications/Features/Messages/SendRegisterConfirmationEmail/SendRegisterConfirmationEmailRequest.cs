using ErrorOr;
using MediatR;

namespace Sample.Modules.Communications.Features.Messages.SendRegisterConfirmationEmail;

internal sealed record SendRegisterConfirmationEmailRequest(
    Guid RecipientId,
    string Email,
    string ActivationLink) : IRequest<ErrorOr<Success>>;