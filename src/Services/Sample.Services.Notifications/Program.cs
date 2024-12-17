using System.Reflection;
using Sample.Aspire.ServiceDefaults;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.Database;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.FastEndpoints;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.MassTransit;
using Sample.Services.Notifications.Database;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<NotificationsServiceDbContext>("sample-notifications-service-db");

builder.Services.AddFastEndpointsConfiguration();
builder.Services.AddFastEndpointsSwaggerDocumentation();
builder.Services.AddMassTransitConfiguration<NotificationsServiceDbContext>(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseServiceDefaults();
// app.UseFastEndpointsConfiguration();

if (app.Environment.IsDevelopment())
{
    // app.UseFastEndpointsSwaggerDocumentation();
    await app.RunDatabaseMigrationsAsync<NotificationsServiceDbContext>();
}

app.Run();