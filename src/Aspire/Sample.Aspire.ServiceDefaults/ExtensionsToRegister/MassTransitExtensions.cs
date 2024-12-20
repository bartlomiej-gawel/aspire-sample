using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Aspire.ServiceDefaults.ExtensionsToRegister;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitConfiguration<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly) where TDbContext : DbContext
    {
        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.UsingRabbitMq((context, rabbitmqConfigurator) =>
            {
                rabbitmqConfigurator.Host(
                    new Uri(configuration["Rabbit:Host"]!),
                    rabbitmqHostConfigurator =>
                    {
                        rabbitmqHostConfigurator.Username(configuration["Rabbit:User"]!);
                        rabbitmqHostConfigurator.Password(configuration["Rabbit:Password"]!);
                    });
                rabbitmqConfigurator.ConfigureEndpoints(context);
            });
            configurator.AddEntityFrameworkOutbox<TDbContext>(outboxConfigurator =>
            {
                outboxConfigurator.UsePostgres();
                outboxConfigurator.UseBusOutbox();
            });
            configurator.AddConsumers(assembly);
        });

        return services;
    }
}