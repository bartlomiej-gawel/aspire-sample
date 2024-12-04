using Sample.Aspire.ServiceDefaults;
using Sample.Services.Organizations.Database;
using Sample.Services.Organizations.Extensions.Database;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<OrganizationsServiceDbContext>("sample-organizations-service-db");
builder.AddServiceDefaults();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    await app.RunDatabaseMigrationsAsync<OrganizationsServiceDbContext>();

app.UseServiceDefaults();

app.Run();