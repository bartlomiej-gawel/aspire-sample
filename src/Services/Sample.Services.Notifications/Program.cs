using Sample.Aspire.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

//builder.AddNpgsqlDbContext<OrganizationsServiceDbContext>("sample-organizations-service-db");
builder.AddServiceDefaults();

var app = builder.Build();

app.UseServiceDefaults();

app.Run();