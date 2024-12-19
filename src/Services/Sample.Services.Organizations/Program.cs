using System.Reflection;
using Sample.Aspire.ServiceDefaults;
using Sample.Services.Organizations.Database;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<OrganizationsServiceDbContext>("sample-organizations-service-db");

// builder.Services.AddFastEndpointsConfiguration();
// builder.Services.AddSwaggerDocumentation();

builder.Services.AddMassTransitConfiguration<OrganizationsServiceDbContext>(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseServiceDefaults();
// app.UseFastEndpointsConfiguration();

if (app.Environment.IsDevelopment())
{
    // app.UseSwaggerDocumentation();
    await app.RunDatabaseMigrationsAsync<OrganizationsServiceDbContext>();
}

app.Run();