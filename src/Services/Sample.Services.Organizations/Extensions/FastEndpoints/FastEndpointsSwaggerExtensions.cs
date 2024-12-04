using FastEndpoints.Swagger;

namespace Sample.Services.Organizations.Extensions.FastEndpoints;

public static class FastEndpointsSwaggerExtensions
{
    public static IServiceCollection AddFastEndpointsSwaggerExtensions(this IServiceCollection services)
    {
        services.SwaggerDocument();

        return services;
    }

    public static WebApplication UseFastEndpointsSwaggerExtensions(this WebApplication app)
    {
        app.UseSwaggerGen();

        return app;
    }
}