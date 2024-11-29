using Sample.Aspire.ServiceDefaults;
using Sample.Services.Organizations.Database;

var builder = WebApplication.CreateBuilder(args);

//builder.AddNpgsqlDbContext<OrganizationsServiceDbContext>("sample-organizations-service-db");
builder.AddServiceDefaults();

var app = builder.Build();

app.UseServiceDefaults();

app.Run();