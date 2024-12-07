using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Services.Organizations.Features.Employees;

namespace Sample.Services.Organizations.Database.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.LocationId)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.Surname)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.Phone)
            .HasMaxLength(15)
            .IsRequired();
    }
}