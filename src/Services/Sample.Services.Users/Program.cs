using System.Reflection;
using Sample.Aspire.ServiceDefaults;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.Database;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.FastEndpoints;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.MassTransit;
using Sample.Services.Users.Database;
using Sample.Services.Users.Features.ActivationTokens;
using Sample.Services.Users.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<UsersServiceDbContext>("sample-users-service-db");

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddScoped<TokenProvider>();
builder.Services.AddScoped<ActivationTokenLinkFactory>();

builder.Services.AddFastEndpointsConfiguration();
builder.Services.AddFastEndpointsSwaggerDocumentation();
builder.Services.AddMassTransitConfiguration<UsersServiceDbContext>(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseServiceDefaults();
app.UseFastEndpointsConfiguration();

if (app.Environment.IsDevelopment())
{
    app.UseFastEndpointsSwaggerDocumentation();
    await app.RunDatabaseMigrationsAsync<UsersServiceDbContext>();
}

app.Run();