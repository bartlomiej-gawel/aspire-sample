using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Aspire.ServiceDefaults.OptionalExtensions.FastEndpoints;

public static class FastEndpointsSwaggerExtensions
{
    public static IServiceCollection AddFastEndpointsSwaggerDocumentation(this IServiceCollection services)
    {
        services.SwaggerDocument(options =>
        {
            options.ShortSchemaNames = true;
            options.DocumentSettings = settings =>
            {
                settings.Title = "Service name";
                settings.Version = "v1";
            };
        });

        return services;
    }

    public static WebApplication UseFastEndpointsSwaggerDocumentation(this WebApplication app)
    {
        app.UseSwaggerGen();

        return app;
    }
}