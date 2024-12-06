using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sample.Aspire.ServiceDefaults.OptionalExtensions.FastEndpoints.Processors;

namespace Sample.Aspire.ServiceDefaults.OptionalExtensions.FastEndpoints;

public static class FastEndpointsExtensions
{
    public static IServiceCollection AddFastEndpointsConfiguration(this IServiceCollection services)
    {
        services.AddFastEndpoints();

        return services;
    }

    public static WebApplication UseFastEndpointsConfiguration(this WebApplication app)
    {
        app.UseFastEndpoints(config =>
        {
            config.Errors.UseProblemDetails();
            config.Endpoints.Configurator = endpointDefinition =>
            {
                if (endpointDefinition.ResDtoType.IsAssignableTo(typeof(IErrorOr)))
                {
                    endpointDefinition.DontAutoSendResponse();
                    endpointDefinition.PostProcessor<ErrorOrPostProcessor>(Order.After);
                    endpointDefinition.Description(builder => builder
                        .ClearDefaultProduces()
                        .Produces(200, endpointDefinition.ResDtoType.GetGenericArguments()[0])
                        .ProducesProblemDetails());
                }
            };
        });

        return app;
    }
}