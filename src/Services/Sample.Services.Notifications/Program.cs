using System.Reflection;
using Sample.Aspire.ServiceDefaults;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.Database;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.MassTransit;
using Sample.Services.Notifications.Database;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<NotificationsServiceDbContext>("sample-notifications-service-db");

var massTransitRabbitOptions = new MassTransitRabbitOptions();
builder.Configuration.GetSection(MassTransitRabbitOptions.SectionName).Bind(massTransitRabbitOptions);
builder.Services.AddMassTransitConfiguration<NotificationsServiceDbContext>(massTransitRabbitOptions, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseServiceDefaults();

if (app.Environment.IsDevelopment())
    await app.RunDatabaseMigrationsAsync<NotificationsServiceDbContext>();

app.Run();