using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Aspire.ServiceDefaults.OptionalExtensions.MassTransit;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitConfiguration<TDbContext>(
        this IServiceCollection services,
        MassTransitRabbitOptions options,
        Assembly currentAssembly) where TDbContext : DbContext
    {
        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.UsingRabbitMq((context, rabbitmqConfigurator) =>
            {
                rabbitmqConfigurator.Host(
                    new Uri(options.Host),
                    rabbitmqHostConfigurator =>
                    {
                        rabbitmqHostConfigurator.Username(options.Username);
                        rabbitmqHostConfigurator.Password(options.Password);
                    });
                rabbitmqConfigurator.ConfigureEndpoints(context);
            });
            configurator.AddEntityFrameworkOutbox<TDbContext>(outboxConfigurator =>
            {
                outboxConfigurator.UsePostgres();
                outboxConfigurator.UseBusOutbox();
            });
            configurator.AddConsumers(currentAssembly);
        });

        return services;
    }
}