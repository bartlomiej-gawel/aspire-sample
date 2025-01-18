using ErrorOr;
using MediatR;

namespace Sample.Modules.Organizations.Features.Organizations.InitializeOrganizationFromRegistration;

internal sealed record InitializeOrganizationFromRegistrationRequest(
    Guid UserId,
    string Name,
    string Surname,
    string Email,
    string Phone,
    Guid OrganizationId,
    string OrganizationName) : IRequest<ErrorOr<Success>>;