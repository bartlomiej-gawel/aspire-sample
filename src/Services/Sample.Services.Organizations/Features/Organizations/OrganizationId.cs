namespace Sample.Services.Organizations.Features.Organizations;

public sealed record OrganizationId
{
    public Guid Value { get; }

    private OrganizationId(Guid value) => Value = value;

    public static OrganizationId Create() => new(Guid.CreateVersion7());
    public static OrganizationId From(Guid organizationId) => new(organizationId);

    public static implicit operator OrganizationId(Guid organizationId) => new(organizationId);
    public static implicit operator Guid(OrganizationId organizationId) => organizationId.Value;
}