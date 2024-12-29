// using Sample.Services.Organizations.Features.Locations;
// using Sample.Services.Organizations.Features.Positions;
//
// namespace Sample.Services.Organizations.Features.Organizations;
//
// public sealed class Organization
// {
//     public Guid Id { get; init; }
//     public string Name { get; set; } = null!;
//     public OrganizationStatus Status { get; set; }
//     public DateTime CreatedAt { get; init; }
//     public DateTime? UpdatedAt { get; set; }
//     public ICollection<Location> Locations { get; init; } = [];
//     public ICollection<Position> Positions { get; init; } = [];
//
//     // private Organization()
//     // {
//     // }
//     //
//     // private Organization(
//     //     Guid organizationId,
//     //     string organizationName)
//     // {
//     //     Id = organizationId;
//     //     Name = organizationName;
//     //     Status = OrganizationStatus.Inactive;
//     //     CreatedAt = DateTime.UtcNow;
//     //     UpdatedAt = null;
//     // }
//     //
//     // public static Organization Initialize(
//     //     Guid organizationId,
//     //     string organizationName)
//     // {
//     //     return new Organization(
//     //         organizationId,
//     //         organizationName);
//     // }
//     //
//     // public ErrorOr<Success> Activate()
//     // {
//     //     if (Status == OrganizationStatus.Active)
//     //         return OrganizationErrors.AlreadyActivated;
//     //     
//     //     Status = OrganizationStatus.Active;
//     //     UpdatedAt = DateTime.UtcNow;
//     //     
//     //     return Result.Success;
//     // }
// }