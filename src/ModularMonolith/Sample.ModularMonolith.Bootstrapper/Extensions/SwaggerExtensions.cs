using FastEndpoints.Swagger;

namespace Sample.ModularMonolith.Bootstrapper.Extensions;

internal static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.SwaggerDocument(options =>
        {
            options.ShortSchemaNames = true;
            options.DocumentSettings = settings =>
            {
                settings.Title = "Bootstrapper";
                settings.Version = "v1";
            };
        });

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        app.UseSwaggerGen();

        return app;
    }
}