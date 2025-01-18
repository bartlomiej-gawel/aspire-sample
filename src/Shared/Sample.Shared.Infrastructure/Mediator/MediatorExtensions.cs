using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sample.Shared.Infrastructure.Mediator.Behaviors;

namespace Sample.Shared.Infrastructure.Mediator;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediatorConfiguration(
        this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        services.AddMediatR(configuration =>
        {
            configuration.AddOpenBehavior(typeof(ExceptionBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

            configuration.RegisterServicesFromAssemblies(moduleAssemblies);
        });

        services.AddValidatorsFromAssemblies(
            moduleAssemblies,
            includeInternalTypes: true);

        return services;
    }
}