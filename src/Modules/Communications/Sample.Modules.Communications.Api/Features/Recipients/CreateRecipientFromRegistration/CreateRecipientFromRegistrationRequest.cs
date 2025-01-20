using ErrorOr;
using MediatR;

namespace Sample.Modules.Communications.Api.Features.Recipients.CreateRecipientFromRegistration;

internal sealed record CreateRecipientFromRegistrationRequest(
    Guid UserId,
    string Name,
    string Surname,
    string Email,
    string Phone,
    Guid OrganizationId,
    string OrganizationName,
    string ActivationLink) : IRequest<ErrorOr<Success>>;