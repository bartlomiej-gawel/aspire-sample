using Sample.Aspire.ServiceDefaults;
using Sample.Services.Users.Database;
using Sample.Services.Users.Extensions.Database;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<UsersServiceDbContext>("sample-users-service-db");
builder.AddServiceDefaults();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    await app.RunDatabaseMigrationsAsync<UsersServiceDbContext>();

app.UseServiceDefaults();

app.Run();