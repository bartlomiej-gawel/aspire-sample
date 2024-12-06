using System.Reflection;
using Sample.Aspire.ServiceDefaults;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.Database;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.MassTransit;
using Sample.Services.Organizations.Database;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<OrganizationsServiceDbContext>("sample-organizations-service-db");

// builder.Services.AddFastEndpointsConfiguration();
// builder.Services.AddSwaggerDocumentation();

var massTransitRabbitOptions = new MassTransitRabbitOptions();
builder.Configuration.GetSection(MassTransitRabbitOptions.SectionName).Bind(massTransitRabbitOptions);
builder.Services.AddMassTransitConfiguration<OrganizationsServiceDbContext>(massTransitRabbitOptions, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseServiceDefaults();
// app.UseFastEndpointsConfiguration();

if (app.Environment.IsDevelopment())
{
    // app.UseSwaggerDocumentation();
    await app.RunDatabaseMigrationsAsync<OrganizationsServiceDbContext>();
}

app.Run();