namespace Sample.ModularMonolith.Organizations.Api.Features.Organizations;

internal sealed class Organization
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; init; } = null!;
}