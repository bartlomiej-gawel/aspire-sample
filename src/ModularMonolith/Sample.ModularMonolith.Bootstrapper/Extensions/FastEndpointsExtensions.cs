using ErrorOr;
using FastEndpoints;

namespace Sample.ModularMonolith.Bootstrapper.Extensions;

internal static class FastEndpointsExtensions
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
            // config.Errors.UseProblemDetails();
            // config.Endpoints.Configurator = endpointDefinition =>
            // {
            //     if (endpointDefinition.ResDtoType.IsAssignableTo(typeof(IErrorOr)))
            //     {
            //         endpointDefinition.DontAutoSendResponse();
            //         endpointDefinition.PostProcessor<ErrorOrPostProcessor>(Order.After);
            //         endpointDefinition.Description(builder => builder
            //             .ClearDefaultProduces()
            //             .Produces(200, endpointDefinition.ResDtoType.GetGenericArguments()[0])
            //             .ProducesProblemDetails());
            //     }
            // };
        });

        return app;
    }
}