using Sample.Aspire.ServiceDefaults;
using Sample.Services.Organizations.Database;
using Sample.Services.Organizations.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<OrganizationsServiceDbContext>("sample-organizations-service-db");
builder.AddServiceDefaults();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.ConfigureDatabaseAsync();

app.Run();