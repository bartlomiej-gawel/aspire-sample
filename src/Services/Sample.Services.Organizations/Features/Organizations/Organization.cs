namespace Sample.Services.Organizations.Features.Organizations;

public sealed class Organization
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = null!;
}